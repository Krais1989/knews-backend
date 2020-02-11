using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostRibbons.Entities
{
    public class PostRibbon
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public DateTime CreateDate { get; set; }

        public long AuthorID { get; set; }
        public string AuthorTitle { get; set; }

        public long CommunityID { get; set; }
        public string CommunityTitle { get; set; }
        public string CommunityIconURL { get; set; }

        public string[] Tags { get; set; }
    }
}
