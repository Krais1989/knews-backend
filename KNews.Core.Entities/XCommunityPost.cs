namespace KNews.Core.Entities
{
    public class XCommunityPost
    {
        public long CommunityID { get; set; }
        public long PostID { get; set; }

        public Post Post { get; set; }
        public Community Community { get; set; }


        public XCommunityPost(long postID, long communityID)
        {
            CommunityID = communityID;
            PostID = postID;
        }
    }
}
