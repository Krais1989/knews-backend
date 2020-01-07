using System;

namespace KNews.Core.Services.Shared.Exceptions
{
    public class BaseRequestException : Exception
    {
        public int Code { get; set; }

        public BaseRequestException(int code, string message) : base(message) 
        { 
            Code = code; 
        }
    }
}
