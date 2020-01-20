using KNews.Core.Entities;
using System;

namespace KNews.Core.Services.Comments.Entities
{
    /// <summary>
    /// Комментарий в ленте популярных помментариев
    /// </summary>
    public class CommentForRibbon
    {
        public long ID { get; set; }
        public ECommentStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShortContent { get; set; }
        public long AuthorID { get; set; }
        public string AuthorTitle { get; set; }

        public long PostID { get; set; }
        public string PostTitle { get; set; }

    }
}
