using System;

namespace KNews.Core.Entities
{
    public class Post
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public int AuthorID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EPostStatus Status { get; set; }
        public long CommunityID { get; set; }
        public Community Community { get; set; }
    }
}
