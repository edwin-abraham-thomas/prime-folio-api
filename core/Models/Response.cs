using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }

        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }

        public Error? Error { get; set; }

        public string? ErrorCode => Error?.ErrorCode;

        public T? Result { get; set; }

        public static Response<T> Success(T result)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Result = result
            };
        }

        public static Response<T> Failure(string errorMessage, string errorCode = null, Exception exception = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Error = new Error(errorMessage, errorCode, exception)
            };
        }

        public static Response<T> Failure(Error error)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Error = error
            };
        }

        public static Response<bool> BoolResponse(bool success, string errorMessage = null, string errorCode = null, Exception exception = null)
        {
            return new Response<bool>
            {
                IsSuccess = success,
                Error = success ? null : new Error(errorMessage, errorCode, exception)
            };
        }
    }
}
