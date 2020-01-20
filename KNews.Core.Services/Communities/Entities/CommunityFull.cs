using KNews.Core.Entities;

namespace KNews.Core.Services.Communities.Entities
{
    public class CommunityFull
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus Status { get; set; }
    }
}