namespace KNews.Core.Entities
{

    public class XCommunityUser
    {
        public long UserID { get; set; }
        public long CommunityID { get; set; }

        public EUserMembershipStatus Status { get; set; }

        public User User { get; set; }
        public Community Community { get; set; }

        public XCommunityUser() { }

        public XCommunityUser(long userId, long communityId, EUserMembershipStatus type) 
        {
            UserID = userId;
            CommunityID = communityId;
            Status = type;
        }
    }
}
