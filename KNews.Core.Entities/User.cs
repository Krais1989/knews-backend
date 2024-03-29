﻿using System.Collections.Generic;

namespace KNews.Core.Entities
{
    public enum EUserStatus
    {
        None,
        Created,
        Approved,
        Banned,
        Deleted
    }

    public class Address
    {

    }

    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string About { get; set; }

        public EUserStatus Status { get; set; }

        public string ImageHash { get; set; }

        public IEnumerable<XCommunityUser> XCommunityUsers { get; set; }
        public IEnumerable<UserInvitation> Invitations { get; set; }
    }
}
