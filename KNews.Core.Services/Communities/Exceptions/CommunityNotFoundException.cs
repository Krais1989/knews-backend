using System;

namespace KNews.Core.Services.Communities.Exceptions
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