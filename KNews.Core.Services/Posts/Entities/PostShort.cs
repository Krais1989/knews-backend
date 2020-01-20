using System;

namespace KNews.Core.Services.Posts.Entities
{
    public class PostShort
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }

        public DateTime CreateDate { get; set; }
        public string CommunityTitle { get; set; }
        public string AuthorTitle { get; set; }
    }
}
