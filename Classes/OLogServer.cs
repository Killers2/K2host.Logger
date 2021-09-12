/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;
using System.Linq;

using K2host.Core;
using K2host.Logger.Enums;
using K2host.Logger.Delegates;
using K2host.Logger.Interfaces;

using K2host.Logger.Extentions;
using K2host.Threading.Interface;

namespace K2host.Logger.Classes
{

    public class OLogServer : ILogServer
    {

        /// <summary>
        /// 
        /// </summary>
        public IThreadManager ThreadManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OLogType LogType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string LogFileDirectory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int MaxEntries { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public OLogFrame FrameType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int RemoveAfter { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool RemoveEnabled { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ILogEntrie[] Rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OnRetryAddEvent OnLogEntryCreated { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public OnLogCreatedEvent OnLogCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public OLogServer(IThreadManager e)
        {
            IsDisposing         = false;
            ThreadManager       = e;
            LogType             = OLogType.CSV;
            LogFileDirectory    = string.Empty;
            MaxEntries          = 100;
            FrameType           = OLogFrame.DAYS;
            RemoveAfter         = 1;
            RemoveEnabled       = false;
            Rows                = Array.Empty<ILogEntrie>();
        }
        
        #region Destructor

        bool IsDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
                if (disposing)
                {
                    IsDisposing = true;

                    if (RemoveEnabled)
                        this.RemoveLogs();

                    if ((Rows.Any()))
                    {
                        this.WriteLog(Rows);

                        Rows.ForEach(i => {
                            if (i != null)
                                i.Dispose();
                        });

                    }

                    Rows.Dispose(out _);
                    Rows = null;

                    OnLogEntryCreated = null;

                    OnLogCreated = null;

                    IsDisposing = false;
                }

            IsDisposed = true;
        }

        #endregion

    }

}
