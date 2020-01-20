using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Comments.Entities
{
    /// <summary>
    /// Комментарий под постом
    /// </summary>
    public class CommentForPost
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

        public string AuthorTitle { get; set; }
    }
}
