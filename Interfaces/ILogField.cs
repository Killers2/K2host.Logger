/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;

namespace K2host.Logger.Interfaces
{

    /// <summary>
    /// A user class helper can be created from this interface for use of any ERP Engine.
    /// </summary>
    public interface ILogField : IDisposable
    {
        
        /// <summary>
        /// The name of the fieid
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The data type of the fieid
        /// </summary>
        Type DataType { get; set; }

    }

}