using KNews.Core.Services.Shared.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Posts.Exceptions
{
    public class PostCreateException : BaseRequestException
    {
        public PostCreateException(int httpCode, int respCode, string message) : base(httpCode, respCode, message)
        {
        }
    }
}
