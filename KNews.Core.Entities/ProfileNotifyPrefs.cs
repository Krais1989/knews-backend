using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Entities
{
    public class ProfileNotifyPrefs
    {
        public long ProfileID { get; set; }

        public bool NotifyPostCreate { get; set; }
        public bool NotifyPostUpdate { get; set; }
        public bool NotifyPostDelete { get; set; }

        public bool NotifyCommentCreate { get; set; }
    }
}
