using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Exceptions
{
    public class CommunityNotFoundException : Exception
    {
        public int CommunityID { get; set; }
        public CommunityNotFoundException()
        {
        }

        public CommunityNotFoundException(string message) : base(message)
        {
        }
    }
}
