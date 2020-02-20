using System;

namespace Binodata.EF.Component.Standard.Error
{
    public class UnitOfWorkException : Exception
    {
        /// <summary>
        /// 例外建構
        /// </summary>
        public UnitOfWorkException() : base()
        {

        }

        public UnitOfWorkException(string message) : base(message)
        {

        }

        public UnitOfWorkException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}