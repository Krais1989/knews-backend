using KNews.Core.Entities;
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
        public int AuthorID { get; set; }        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EPostStatus Status { get; set; }
        public long CommunityID { get; set; }

        public string AuthorTitle { get; set; }
        public string CommunityTitle { get; set; }
    }
}
