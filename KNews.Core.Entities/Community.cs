using System;
using System.Collections.Generic;

namespace KNews.Core.Entities
{
    /// <summary>
    /// Право чтения в сообществе
    /// </summary>
    public enum ECommunityReadPermission : byte
    {
        /// <summary>
        /// Просматривать группу могут все
        /// </summary>
        All = 0,
        /// <summary>
        /// Просматривать группу могут лишь её участники
        /// </summary>
        MembersOnly
    }

    /// <summary>
    /// Право создание постов в сообществе
    /// </summary>
    public enum ECommunityPostCreatePermission : byte
    {
        All,
        MembersOnly,
        ModeratorOnly
    }

    public enum ECommunityPostDeletePermission : byte
    {
        AuthorOnly,
        ModeratorOnly
    }

    public class Community
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus Status { get; set; }
        public ECommunityReadPermission ReadPermissions { get; set; }
        public ECommunityPostCreatePermission CreatePostPermissions { get; set; }
        public ECommunityPostDeletePermission DeletePermissions { get; set; }

        public DateTime CreateDate { get; set; }

        public long MembersCount { get; set; }

        public bool InvitationsAvailable { get; set; }

        public IEnumerable<XCommunityUser> UserCommunities { get; set; }
        public IEnumerable<XCommunityPost> PostCommunities { get; set; }
    }
}
