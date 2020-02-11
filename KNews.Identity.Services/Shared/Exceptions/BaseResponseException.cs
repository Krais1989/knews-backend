using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Shared.Exceptions
{
    public class BaseResponseException : Exception
    {
        public int HttpCode { get; set; }
        public int Code { get; set; }

        public string[] ErrorData { get; set; }

        public BaseResponseException(int code, int httpCode, string message, string[] errorData) : base(message)
        {
            Code = code;
            HttpCode = httpCode;
            ErrorData = errorData;
        }
    }
}
