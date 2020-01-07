using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Posts.Entities
{
    public class PostFull
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
    }
}
