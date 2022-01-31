using System;
using System.Collections.Generic;
using System.Text;

namespace Webdam.Sdk.Exceptions
{
    /// <summary>
    /// Missing token exception. Exception raised when trying to use
    /// <see cref="AssetService"/>  without a valid token
    /// </summary>
    public class MissingTokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingTokenException"/> class.
        /// </summary>
        /// <param name="message">Exception reason</param>
        public MissingTokenException(string message)
            : base(message)
        {
        }
    }
}
