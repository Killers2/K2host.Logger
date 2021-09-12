/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;

using K2host.Logger.Interfaces;

namespace K2host.Logger.Extentions
{

    public static class ILogEntrieExtentions
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToStringCSV(this ILogEntrie e)
        {
            if (e == null)
                return string.Empty;

            return e.DateTime.ToString() + "," + e.ErrorType.ToString() + "," + e.Type.ToString() + "," + e.ServiceName.ToUpper() + "," + e.Message;
       
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToStringTAB(this ILogEntrie e)
        {
            return e.DateTime.ToString() + '\t' + e.ErrorType.ToString() + '\t' + e.Type.ToString() + '\t' + e.ServiceName.ToUpper() + '\t' + e.Message;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToStringPIPE(this ILogEntrie e)
        {
            return e.DateTime.ToString() + "|" + e.ErrorType.ToString() + "|" + e.Type.ToString() + "|" + e.ServiceName.ToUpper() + "|" + e.Message;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static ILogEntrie Clone(this ILogEntrie e)
        {

            ILogEntrie output   =  (ILogEntrie)Activator.CreateInstance(e.GetType());
            output.ErrorType    = e.ErrorType;
            output.Type         = e.Type;
            output.DateTime     = e.DateTime;
            output.ServiceName  = e.ServiceName;
            output.Message      = e.Message;

            return output;

        }

    }

}
