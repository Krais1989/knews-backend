using System;

namespace KNews.Core.Entities
{
    /// <summary>
    /// Изменение поста (для хранения истории)
    /// </summary>
    public class PostStatusChange
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int OldStatus { get; set; }
        public int NewStatus { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
