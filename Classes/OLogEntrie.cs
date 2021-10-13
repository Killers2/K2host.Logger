/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/

using System;
using System.Data;

using K2host.Data.Classes;
using K2host.Data.Attributes;

using K2host.Logger.Enums;
using K2host.Logger.Interfaces;

namespace K2host.Logger.Classes
{

    [Serializable()]
    public class OLogEntrie : ODataObject<OLogEntrie>, ILogEntrie
    {

        /// <summary>
        /// The audit error type
        /// </summary>
        [ODataType(SqlDbType.Int)]
        public OLogAuditError ErrorType { get; set; }
        
        /// <summary>
        /// The log type
        /// </summary>
        [ODataType(SqlDbType.Int)]
        public OLogEntrieType Type { get; set; }
        /// <summary>
        /// The name of the service
        /// </summary>
        [ODataType(SqlDbType.NVarChar, 255)]
        public string ServiceName { get; set; }
       
        /// <summary>
        /// The error message
        /// </summary>
        [ODataType(SqlDbType.NVarChar, 4000)]
        public string Message { get; set; }
        
        /// <summary>
        /// The time stamp of the log
        /// </summary>
        [ODataType(SqlDbType.DateTime)]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// The object constructor
        /// </summary>
        public OLogEntrie()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// The object constructor
        /// </summary>
        public OLogEntrie(OLogAuditError errorType, OLogEntrieType type, DateTime dateTimeStamp, string serviceName, string massage)
            : base(string.Empty)
        {
            ErrorType       = errorType;
            DateTime        = dateTimeStamp;
            ServiceName     = serviceName;
            Message         = massage;
            Type            = type;
        }

        /// <summary>
        /// The object constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public OLogEntrie(string connectionString)
            : base(connectionString)
        {

        }

    }

}
