using System;

namespace KNews.Core.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public ECommentStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public int PostID { get; set; }
        public int? ParentCommentID { get; set; }
    }
}
