using System;

namespace Xamn.Common.Notry
{
    public class TryException : Exception
    {
        public TryException(Exception innerException) : base("This exception is automatically thrown by Xamn.Common.TypeUtility", innerException)
        {
        }
    }
}