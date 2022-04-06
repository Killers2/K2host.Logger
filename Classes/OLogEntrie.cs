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

using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace K2host.Logger.Classes
{

    [Serializable()]
    public class OLogEntrie : ODataObject<OLogEntrie>, ILogEntrie
    {

        /// <summary>
        /// The audit error type
        /// </summary>
        [ODataType(SqlDbType.Int, MySqlDbType.Int32, OracleDbType.Int32)]
        public OLogAuditError ErrorType { get; set; }

        /// <summary>
        /// The log type
        /// </summary>
        [ODataType(SqlDbType.Int, MySqlDbType.Int32, OracleDbType.Int32)]
        public OLogEntrieType Type { get; set; }
        /// <summary>
        /// The name of the service
        /// </summary>
        [ODataType(SqlDbType.NVarChar, MySqlDbType.VarString, OracleDbType.Varchar2, 255)]
        public string ServiceName { get; set; }
       
        /// <summary>
        /// The error message
        /// </summary>
        [ODataType(SqlDbType.NVarChar, MySqlDbType.VarString, OracleDbType.Varchar2, 4000)]
        public string Message { get; set; }

        /// <summary>
        /// The time stamp of the log
        /// </summary>
        [ODataType(SqlDbType.DateTime, MySqlDbType.DateTime, OracleDbType.TimeStamp)]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// The object constructor
        /// </summary>
        public OLogEntrie()
            : base()
        {

        }

        /// <summary>
        /// The object constructor
        /// </summary>
        public OLogEntrie(OLogAuditError errorType, OLogEntrieType type, DateTime dateTimeStamp, string serviceName, string massage)
            : base()
        {
            ErrorType       = errorType;
            DateTime        = dateTimeStamp;
            ServiceName     = serviceName;
            Message         = massage;
            Type            = type;
        }

    }

}
