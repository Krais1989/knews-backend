using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Entities
{
    public class ProfileSendingPrefs
    {
        public long ProfileID { get; set; }

        public bool SendMostPopularPosts { get; set; }
        public bool SendMostPopularComments { get; set; }
    }
}
