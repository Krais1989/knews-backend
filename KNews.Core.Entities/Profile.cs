namespace KNews.Core.Entities
{
    public class Profile
    {
        public long ID { get; set; }
        public long UserID { get; set; }

        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }

        public ProfileNotifyPrefs NotifyPrefs { get; set; }
        public ProfileRibbonPrefs RibbonPrefs { get; set; }
        public ProfileSendingPrefs SendingPrefs { get; set; }
    }
}
