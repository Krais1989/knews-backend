using System;

namespace KNews.Core.Entities
{
    public class Comment
    {
        public long ID { get; set; }
        public long AuthorID { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public ECommentStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public long PostID { get; set; }
        public long? ParentCommentID { get; set; }

        public User Author { get; set; }
        public Post Post { get; set; }
        public Comment ParentComment { get; set; }
    }
}
