using System;

namespace KNews.Core.Entities
{
    public class CommentChange
    {
        public int ID { get; set; }
        public int CommentaryID { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}
