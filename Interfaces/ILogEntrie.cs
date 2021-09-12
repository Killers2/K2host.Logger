/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;

using K2host.Data.Interfaces;
using K2host.Logger.Enums;

namespace K2host.Logger.Interfaces
{

    public interface ILogEntrie : IDataObject
    {
        
        /// <summary>
        /// The audit error type
        /// </summary>
        OLogAuditError ErrorType { get; set; }

        /// <summary>
        /// The log type
        /// </summary>
        OLogEntrieType Type { get; set; }
        
        /// <summary>
        /// The name of the service
        /// </summary>
        string ServiceName { get; set; }
        
        /// <summary>
        /// The error message
        /// </summary>
        string Message { get; set; }
        
        /// <summary>
        /// The time stamp of the log
        /// </summary>
        DateTime DateTime { get; set; }

    }

}