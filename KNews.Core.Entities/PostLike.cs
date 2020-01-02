namespace KNews.Core.Entities
{
    public class PostLike
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
    }
}
