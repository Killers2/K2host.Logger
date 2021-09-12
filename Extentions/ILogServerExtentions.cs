/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.IO;

using Microsoft.VisualBasic.FileIO;

using K2host.Core;
using K2host.Logger.Enums;
using K2host.Logger.Interfaces;
using K2host.Threading.Classes;
using K2host.Threading.Extentions;

using gl = K2host.Core.OHelpers;

namespace K2host.Logger.Extentions
{

    public static class ILogServerExtentions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void Add(this ILogServer e, ILogEntrie l)
        {

            if (e.IsDisposing)
                return;

            e.Rows = e.Rows.Append(l).ToArray();

            e.OnLogEntryCreated?.Invoke(l.Clone());

            if (e.Rows.Length >= e.MaxEntries)
            {

                ILogEntrie[] clones = Array.Empty<ILogEntrie>();

                e.Rows.ForEach(item => {
                    clones = clones.Append(item.Clone()).ToArray();
                    item.Dispose();
                });

                e.ThreadManager.Add(
                    new OThread(
                        new ParameterizedThreadStart( e.WriteLog )
                    )
                ).Start(clones);

                e.Rows.Dispose(out _);
                e.Rows = Array.Empty<ILogEntrie>();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="has_fields_enclosed_in_quotes"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static DataTable GetErrors(this ILogServer _, string path, bool has_fields_enclosed_in_quotes, OLogType t)
        {

            DataTable csvData = new();

            try
            {
                using TextFieldParser csvReader = new(path);
                if (t == OLogType.CSV)
                    csvReader.SetDelimiters(new string[] { "," });

                if (t == OLogType.PIPE)
                    csvReader.SetDelimiters(new string[] { "|" });

                if (t == OLogType.TAB)
                    csvReader.SetDelimiters(new string[] { "\t" });

                csvReader.HasFieldsEnclosedInQuotes = has_fields_enclosed_in_quotes;

                string[] colFields = csvReader.ReadFields();

                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new(column) { AllowDBNull = true };
                    csvData.Columns.Add(datecolumn);
                }

                bool _isError = false;

                while (!csvReader.EndOfData)
                {

                    object[] fieldData = csvReader.ReadFields();

                    _isError = false;

                    for (int i = 0; i < fieldData.Length; i++)
                    {

                        if (fieldData[i].ToString() == "")
                            fieldData[i] = null;

                        if (i == 1)
                            fieldData[i] = ((int)gl.StringToEnum<OLogAuditError>(fieldData[i].ToString())).ToString();

                        if (i == 2)
                        {
                            fieldData[i] = ((int)gl.StringToEnum<OLogType>(fieldData[i].ToString())).ToString();
                            _isError = ((OLogEntrieType)fieldData[i] == OLogEntrieType.FAILED);
                        }

                        //0 = DataTime
                        //1 = ErrorType
                        //2 = Type 
                        //3 = ServiceName
                        //4 = Message

                    }

                    if (_isError)
                        csvData.Rows.Add(fieldData);

                }

            }
            catch { }

            return csvData;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLog(this ILogServer e, object o)
        {
            try
            {

                if (string.IsNullOrEmpty(e.LogFileDirectory))
                    return;

                ILogEntrie[] clones = (ILogEntrie[])o;

                string fileExt = string.Empty;

                switch (e.LogType)
                {
                    case OLogType.CSV:
                        fileExt = ".csv";
                        break;
                    case OLogType.TAB:
                        fileExt = ".txt";
                        break;
                    case OLogType.PIPE:
                        fileExt = ".txt";
                        break;
                }

                string          filename = e.LogFileDirectory + "\\Log_" + gl.UnixTime().ToString() + fileExt;
                FileStream      fs = new(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                StreamWriter    sr = new(fs);

                switch (e.LogType)
                {
                    case OLogType.CSV:
                        sr.WriteLine("DateTime,ErrorType,Type,ServiceName,Message");
                        break;
                    case OLogType.TAB:
                        sr.WriteLine("DateTime" + '\t' + "ErrorType" + '\t' + "Type" + '\t' + "ServiceName" + '\t' + "Message");
                        break;
                    case OLogType.PIPE:
                        sr.WriteLine("DateTime|ErrorType|Type|ServiceName|Message");
                        break;
                }

                foreach (ILogEntrie clone in clones)
                {
                    switch (e.LogType)
                    {
                        case OLogType.CSV:
                            sr.WriteLine(clone.ToStringCSV());
                            break;
                        case OLogType.TAB:
                            sr.WriteLine(clone.ToStringTAB());
                            break;
                        case OLogType.PIPE:
                            sr.WriteLine(clone.ToStringPIPE());
                            break;
                    }

                    clone.Dispose();

                    Thread.Sleep(1);

                }

                sr.Close();
                sr.Dispose();
                sr = null;

                fs.Close();
                fs.Dispose();
                fs = null;

                e.OnLogCreated?.Invoke(clones.ToArray());

                clones.Dispose(out _);
                clones = null;

                if (e.RemoveEnabled)
                    e.RemoveLogs();

            }
            catch
            {

                Thread.Sleep(1000);

                e.WriteLog(o);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RemoveLogs(this ILogServer e)
        {
            DateTime dta = DateTime.Now;
            DirectoryInfo di = new(e.LogFileDirectory);

            switch (e.FrameType)
            {
                case OLogFrame.MINS:
                    dta = DateTime.Now.AddMinutes((e.RemoveAfter * -1));
                    break;
                case OLogFrame.HOURS:
                    dta = DateTime.Now.AddHours((e.RemoveAfter * -1));
                    break;
                case OLogFrame.DAYS:
                    dta = DateTime.Now.AddDays((e.RemoveAfter * -1));
                    break;
                case OLogFrame.MONTHS:
                    dta = DateTime.Now.AddMonths((e.RemoveAfter * -1));
                    break;
                case OLogFrame.YEARS:
                    dta = DateTime.Now.AddYears((e.RemoveAfter * -1));
                    break;
            }


            di.GetFiles().ForEach(fi => {
                try
                {
                    DateTime dtb = gl.UnixTime2DateTime(Convert.ToInt64(fi.Name.Remove(0, (fi.Name.IndexOf("_") + 1)).Replace(fi.Extension, string.Empty)));
                    if (dtb <= dta)
                        fi.Delete();
                } catch { }            
            });

        }

    }

}
