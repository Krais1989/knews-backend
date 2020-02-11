using System;

namespace KNews.Core.Entities
{
    public enum EPostCommentCreatePermissions
    {
        NotAllowed,
        Allowed,
        AllowedAuthenticated,
        AllowedCommunityMembers
    }

    public enum EPostCommentReadPermissions
    {
        NotAllowed,
        Allowed,
        AllowedAuthenticated,
        AllowedCommunityMembers
    }

    public enum EPostCommentUpdatePermissions
    {
        NotAllowed,
        Allowed
    }

    public enum EPostCommentDeletePermissions
    {
        NotAllowed,
        Allowed
    }

    public class Post
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public long AuthorID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EPostStatus Status { get; set; }
        public long CommunityID { get; set; }

        public EPostCommentCreatePermissions CommentCreatePermissions { get; set; }
        public EPostCommentReadPermissions CommentReadPermissions { get; set; }
        public EPostCommentUpdatePermissions CommentUpdatePermissions { get; set; }
        public EPostCommentDeletePermissions CommentDeletePermissions { get; set; }
        
        public Community Community { get; set; }
        public User Author { get; set; }
    }
}
