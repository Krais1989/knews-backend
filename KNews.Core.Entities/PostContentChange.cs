namespace KNews.Core.Entities
{
    public class PostContentChange
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public string OldContent { get; set; }
        public string NewContent { get; set; }

    }
}
