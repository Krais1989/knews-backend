using KNews.Core.Services.Shared.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Posts.Exceptions
{
    public class PostCreateException : BaseRequestException
    {
        public PostCreateException(int code, string message) : base(code, message)
        {
        }
    }
}
