using System;

namespace KNews.Core.Services.Shared.Exceptions
{
    public class BaseRequestException : Exception
    {
        public int HttpCode { get; set; }
        public int ResponseCode { get; set; }

        public BaseRequestException(int httpCode, int respCode, string message) : base(message) 
        { 
            HttpCode = httpCode;
            ResponseCode = respCode;
        }
    }
}
