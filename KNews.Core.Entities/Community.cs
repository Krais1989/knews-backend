using System;
using System.Collections.Generic;

namespace KNews.Core.Entities
{
    public enum ECommunityPrivacyType : byte
    {
        Open = 0,
        Closed
    }

    public class Community
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus Status { get; set; }
        public ECommunityPrivacyType PrivacyType { get; set; }
        public DateTime CreateDate { get; set; }

        public long MembersCount { get; set; }

        public bool InvitationsAvailable { get; set; }

        public IEnumerable<XCommunityUser> UserCommunities { get; set; }
        public IEnumerable<XCommunityPost> PostCommunities { get; set; }
    }
}
