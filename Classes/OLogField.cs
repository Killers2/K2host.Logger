/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/
using System;

using K2host.Logger.Interfaces;

namespace K2host.Logger.Enums
{

    public class OLogField : ILogField
    {

        /// <summary>
        /// The name of the fieid
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The data type of the fieid
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// The constructor for creating an instance
        /// </summary>
        public OLogField()
        {
        }
        
        #region Deconstuctor

        private bool IsDisposed = false;

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




                }
            IsDisposed = true;
        }

        #endregion

    }

}
