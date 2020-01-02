using System;

namespace KNews.Core.Entities
{
    /// <summary>
    /// Приглашение юзера в сообщество
    /// </summary>
    public class UserInvitation
    {
        public long ID { get; set; }
        /// <summary>
        /// Приглашающий
        /// </summary>
        public long InvitingUserID { get; set; }
        /// <summary>
        /// Приглашаемый
        /// </summary>
        public long InvitedUserID { get; set; }
        public long CommunityID { get; set; }
        public EUserInvitationStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
