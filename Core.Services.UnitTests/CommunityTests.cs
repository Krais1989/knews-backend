using KNews.Core.Entities;
using KNews.Core.Services.CommunityRequests;
using KNews.Core.Services.CommunityValidators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests
{
    public class CommunityTests
    {
        [Test]
        public async ValueTask GetFullValidation()
        {
            var request = new CommunityGetFullRequest()
            {
                IDs = new long[] { 232 }
            };
            var validator = new CommunityGetFullValidator();
            var result = await validator.ValidateAsync(request);
            Assert.True(result.IsValid);
        }

        [Test]
        public async ValueTask GetShortValidation()
        {
            var request = new CommunityGetShortRequest()
            {
                IDs = new long[] { 232 }
            };
            var validator = new CommunityGetShortValidator();
            var result = await validator.ValidateAsync(request);
            Assert.True(result.IsValid);
        }

        private static List<Tuple<CommunityCreateRequest, bool>> CommunityCreateRequestCases
           => new List<Tuple<CommunityCreateRequest, bool>>
           {
                new Tuple<CommunityCreateRequest, bool>(new CommunityCreateRequest() { Title = "Title", Description = "Description", OwnerID = 123, Rules = "Rules" }, true),
                new Tuple<CommunityCreateRequest, bool>(new CommunityCreateRequest() { }, false)
           };

        [Test, TestCaseSource(nameof(CommunityCreateRequestCases))]
        public async ValueTask CreateValidation(Tuple<CommunityCreateRequest, bool> data)
        {
            var validator = new CommunityCreateValidator();
            var result = await validator.ValidateAsync(data.Item1);
            Assert.AreEqual(result.IsValid, data.Item2);
        }

        private static List<Tuple<CommunityUpdateRequest, bool>> CommunityUpdateRequestCases
            => new List<Tuple<CommunityUpdateRequest, bool>>
            {
                new Tuple<CommunityUpdateRequest, bool>(
                    new CommunityUpdateRequest()
                    {
                        ID = 1,
                        Title = "New Title",
                        Description = "New Description",
                        Rules = "New Rules",
                        Status = ECommunityStatus.Approved,
                        SaveChanges = true,
                    }, true),
                new Tuple<CommunityUpdateRequest, bool>(
                    new CommunityUpdateRequest()
                    {
                        SaveChanges = true,
                    }, false)
            };

        [Test, TestCaseSource(nameof(CommunityUpdateRequestCases))]
        public async ValueTask UpdateValidation(Tuple<CommunityUpdateRequest, bool> data)
        {
            var validator = new CommunityUpdateValidator();
            var result = await validator.ValidateAsync(data.Item1);
            Assert.AreEqual(result.IsValid, data.Item2);
        }

        [Test]
        public async ValueTask DeleteValidation()
        {
            var request = new CommunityDeleteRequest()
            {
                ID = 555,
                SaveChanges = true
            };
            var validator = new CommunityDeleteValidator();
            var result = await validator.ValidateAsync(request);
            Assert.True(result.IsValid);
        }

        [Test, Combinatorial]
        public async ValueTask UserJoinValidation_TrueCombinations(
            [Values(EUserStatus.Approved)] EUserStatus userStatus,
            [Values(ECommunityStatus.Approved)] ECommunityStatus communityStatus,
            [Values(false)] bool existInvitation,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? xCommunityUserType
            )
        {
            await UserJoinValidation(userStatus, communityStatus, existInvitation, xCommunityUserType, true);
        }

        [Test, Combinatorial]
        public async ValueTask UserJoinValidation_FalseCombinations(
            [Values(EUserStatus.Created, EUserStatus.Deleted, EUserStatus.Banned)] EUserStatus userStatus,
            [Values(ECommunityStatus.Created, ECommentStatus.Deleted)] ECommunityStatus communityStatus,
            [Values(false, true)] bool existInvitation,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)] EXUserCommunityType? xCommunityUserType
            )
        {
            await UserJoinValidation(userStatus, communityStatus, existInvitation, xCommunityUserType, false);
        }

        public async ValueTask UserJoinValidation(
            EUserStatus userStatus,
            ECommunityStatus communityStatus,
            bool existInvitation,
            EXUserCommunityType? xCommunityUserType,
            bool verify
            )
        {
            var user = new User()
            {
                ID = 1,
                Status = userStatus
            };
            var community = new Community()
            {
                ID = 1,
                Status = communityStatus
            };
            var invitations = new List<UserInvitation> { };
            if (existInvitation)
                invitations.Add(new UserInvitation() { ID = 1, CreateDate = DateTime.UtcNow, CommunityID = 1, InvitedUserID = 1, InvitingUserID = 10 });
            var xCommunityUsers = xCommunityUserType.HasValue
                ? new List<XCommunityUser> { new XCommunityUser(user.ID, community.ID, xCommunityUserType.Value) }
                : new List<XCommunityUser> { };

            var validatorDto = new CommunityUserJoinValidatorDto()
            {
                User = user,
                Community = community,
                UserInvitations = invitations,
                XCommunityUsers = xCommunityUsers
            };

            var validator = new CommunityUserJoinValidator();
            var result = await validator.ValidateAsync(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        [Test, Combinatorial]
        public async ValueTask UserInviteValidation_FalseCombinations(
            [Values(EUserStatus.Banned, EUserStatus.Created, EUserStatus.Deleted)] EUserStatus invitingStatus,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.None, null)] EXUserCommunityType? invitingMemberStatus,
            [Values(EUserStatus.Banned, EUserStatus.Created, EUserStatus.Deleted)] EUserStatus invitedStatus,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)]EXUserCommunityType? invitedMemberStatus,
            [Values(ECommunityStatus.Created, ECommunityStatus.Deleted)]ECommunityStatus communityStatus,
            [Values(EUserInvitationStatus.Accepted, EUserInvitationStatus.Declined, EUserInvitationStatus.Recieved)] EUserInvitationStatus? existInvitationStatus,
            [Values(true, false)]bool communityInvitationsAvailable)
        {
            await UserInviteValidation(
                invitingStatus,
                invitingMemberStatus,
                invitedStatus,
                invitedMemberStatus,
                communityStatus,
                existInvitationStatus,
                communityInvitationsAvailable,
                false);
        }

        [Test, Combinatorial]
        public async ValueTask UserInviteValidation_TrueCombinations(
            [Values(EUserStatus.Approved)] EUserStatus invitingStatus,
            [Values(EXUserCommunityType.Moderator)] EXUserCommunityType? invitingMemberStatus,
            [Values(EUserStatus.Approved)] EUserStatus invitedStatus,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? invitedMemberStatus,
            [Values(ECommunityStatus.Approved)]ECommunityStatus communityStatus,
            [Values(null, EUserInvitationStatus.Ignored)] EUserInvitationStatus? existInvitationStatus,
            [Values(true)]bool communityInvitationsAvailable)
        {
            await UserInviteValidation(
                invitingStatus,
                invitingMemberStatus,
                invitedStatus,
                invitedMemberStatus,
                communityStatus,
                existInvitationStatus,
                communityInvitationsAvailable,
                true);
        }

        public async ValueTask UserInviteValidation(
            [Values(EUserStatus.Approved)] EUserStatus invitingStatus,
            [Values(EXUserCommunityType.Moderator)] EXUserCommunityType? invitingMemberStatus,
            [Values(EUserStatus.Approved)] EUserStatus invitedStatus,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? invitedMemberStatus,
            [Values(ECommunityStatus.Approved)]ECommunityStatus communityStatus,
            [Values(null, EUserInvitationStatus.Ignored)] EUserInvitationStatus? existInvitationStatus,
            [Values(true)]bool communityInvitationsAvailable,
            [Values(true)]bool verify)
        {
            var invitingUser = new User() { ID = 1, Status = invitingStatus };
            var invitingXUserCommunity = invitingMemberStatus.HasValue
                ? new XCommunityUser(1, 1, invitingMemberStatus.Value)
                : null;

            var invitedUser = new User { ID = 2, Status = invitedStatus };
            var invitedXUserCommunity = invitedMemberStatus.HasValue
                ? new XCommunityUser(2, 1, invitedMemberStatus.Value)
                : null;
            var invitedExistInvitations = existInvitationStatus.HasValue
                ? new List<UserInvitation> {
                    new UserInvitation()
                    {
                        ID = 1,
                        InvitingUserID = 1,
                        InvitedUserID = 2,
                        CommunityID = 1,
                        Status = existInvitationStatus.Value
                    }}
                : new List<UserInvitation>();

            var community = new Community { ID = 1, InvitationsAvailable = communityInvitationsAvailable, Status = communityStatus };

            var request = new CommunityUserInviteValidatorDto(
                invitingUser,
                invitedUser,
                community,
                invitedExistInvitations,
                invitedXUserCommunity,
                invitingXUserCommunity
                );

            var validator = new CommunityUserInviteValidator();
            var result = await validator.ValidateAsync(request);
            Assert.AreEqual(result.IsValid, verify);
        }

        [Test, Combinatorial]
        public async ValueTask UserLeaveValidation_TrueCombs(
            [Values(EUserStatus.Approved)] EUserStatus userStatus,
            [Values(ECommunityStatus.Approved, ECommunityStatus.Created, ECommunityStatus.Deleted)] ECommunityStatus comStatus,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)]EXUserCommunityType? xUserCommunityStatus)
        {
            await UserLeaveValidation(userStatus, comStatus, xUserCommunityStatus, true);
        }

        [Test, Combinatorial]
        public async ValueTask UserLeaveValidation_FalseCombs(
            [Values(EUserStatus.Banned, EUserStatus.Created, EUserStatus.Deleted)] EUserStatus userStatus,
            [Values(ECommunityStatus.Approved, ECommunityStatus.Created, ECommunityStatus.Deleted)] ECommunityStatus comStatus,
            [Values(null, EXUserCommunityType.None)]EXUserCommunityType? xUserCommunityStatus)
        {
            await UserLeaveValidation(userStatus, comStatus, xUserCommunityStatus, false);
        }

        public async ValueTask UserLeaveValidation(
            EUserStatus userStatus,
            ECommunityStatus comStatus,
            EXUserCommunityType? xUserCommunityStatus,
            bool verify)
        {
            var user = new User { ID = 1, Status = userStatus };
            var community = new Community { ID = 1, Status = comStatus };
            var xUserCommunity = xUserCommunityStatus.HasValue
                ? new List<XCommunityUser> { new XCommunityUser(1, 1, xUserCommunityStatus.Value) }
                : new List<XCommunityUser>();

            var leaveValidatorDto = new CommunityUserLeaveValidatorDto(user, community, xUserCommunity);

            var validator = new CommunityUserLeaveValidator();
            var result = await validator.ValidateAsync(leaveValidatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }
    }
}