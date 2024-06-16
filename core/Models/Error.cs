using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Error
    {
        public Error(string errorMessage, string errorCode = null, Exception exception = null)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Exception = exception;
        }
        public string ErrorMessage { get; set; }

        public string ErrorCode { get; set; }

        public Exception Exception { get; set; }
    }
}
