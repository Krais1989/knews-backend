using System;
using System.Collections.Generic;

namespace KNews.Core.Entities
{
    /// <summary>
    /// Право чтения в сообществе
    /// </summary>
    public enum ECommunityReadPermissions : byte
    {
        /// <summary>
        /// Все включая гостей
        /// </summary>
        All = 0,
        /// <summary>
        /// Все прошедний аутентификацию
        /// </summary>
        Authenticated,
        /// <summary>
        /// Участники группы
        /// </summary>
        Members
    }

    /// <summary>
    /// Право создание постов в сообществе
    /// </summary>
    public enum ECommunityPostCreatePermissions : byte
    {
        Authenticated,
        Members,
        Moderators
    }

    public enum ECommunityPostDeletePermissions : byte
    {
        Authors,
        Moderators
    }

    public enum ECommunityInvitationPermissions : byte
    {
        NoOne,
        Members,
        Moderators
    }

    public enum ECommunityJoinPermissions: byte
    {
        Allowed,
        NotAllowed,
        ByInvitation,
    }

    public class Community
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus Status { get; set; }
        public ECommunityReadPermissions ReadPermissions { get; set; }
        public ECommunityPostCreatePermissions CreatePostPermissions { get; set; }
        public ECommunityPostDeletePermissions DeletePermissions { get; set; }
        public ECommunityJoinPermissions JoinPermissions { get; set; }

        public DateTime CreateDate { get; set; }

        public ECommunityInvitationPermissions InvitationPermissions { get; set; }

        public IEnumerable<XCommunityUser> UserCommunities { get; set; }
        public IEnumerable<XCommunityPost> PostCommunities { get; set; }

        public long MembersCount { get; set; }
    }
}
