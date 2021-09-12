/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/

using System;

using K2host.Logger.Delegates;
using K2host.Logger.Enums;
using K2host.Threading.Interface;

namespace K2host.Logger.Interfaces
{

    /// <summary>
    /// A user class helper can be created from this interface for use of any ERP Engine.
    /// </summary>
    public interface ILogServer : IDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        IThreadManager ThreadManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        OLogType LogType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LogFileDirectory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MaxEntries { get; set; }

        /// <summary>
        /// 
        /// </summary>
        OLogFrame FrameType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int RemoveAfter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool RemoveEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILogEntrie[] Rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        OnRetryAddEvent OnLogEntryCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        OnLogCreatedEvent OnLogCreated { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsDisposing { get; set; }

    }

}