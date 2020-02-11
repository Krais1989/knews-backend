using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Entities
{
    public class ProfileFull
    {
        public long ID { get; set; }
        public long UserID { get; set; }

        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public ProfileNotifyPrefs NotifyPrefs { get; set; }
        public ProfileRibbonPrefs RibbonPrefs { get; set; }
        public ProfileSendingPrefs SendingPrefs { get; set; }
    }
}
