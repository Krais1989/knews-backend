using KNews.Core.Entities;
using KNews.Core.Services.Posts.Handlers;
using KNews.Core.Services.Posts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests
{
    public class PostTests
    {
        [Test, Combinatorial]
        public async ValueTask GetFullValidation_TrueCases(
            [Values(EPostStatus.Approved)] EPostStatus postStatus,
            [Values(ECommunityReadPermission.All)] ECommunityReadPermission communityReadPermission,
            [Values(EXUserCommunityType.Member)] EXUserCommunityType? curUserMembership,
            [Values(true)] bool curUserIsAuthor,
            [Values(true)] bool verify)
        {
            await GetFullValidation(postStatus, communityReadPermission, curUserMembership, curUserIsAuthor, verify);
        }

        [Test, Combinatorial]
        public async ValueTask GetFullValidation_FalseCases(
            [Values(EPostStatus.Forbiden, EPostStatus.Deleted)] EPostStatus postStatus,
            [Values(ECommunityReadPermission.MembersOnly)] ECommunityReadPermission communityReadPermission,
            [Values(EXUserCommunityType.None)] EXUserCommunityType? curUserMembership,
            [Values(false)] bool curUserIsAuthor,
            [Values(false)] bool verify)
        {
            await GetFullValidation(postStatus, communityReadPermission, curUserMembership, curUserIsAuthor, verify);
        }

        private async ValueTask GetFullValidation(
            EPostStatus postStatus,
            ECommunityReadPermission communityReadPermission,
            EXUserCommunityType? curUserMembership,
            bool getByAuthor,
            bool verify)
        {
            var request = new PostGetFullValidatorDto()
            {
                CommunityReadPermissions = communityReadPermission,
                MemberStatus = curUserMembership,
                PostStatus = postStatus,
                GetByAuthor = getByAuthor,
            };

            var validator = new PostGetFullValidator();
            var result = await validator.ValidateAsync(request);
            Assert.AreEqual(result.IsValid, verify);
        }


        [Test, Combinatorial]
        public async ValueTask GetShortValidation_TrueCases(
            [Values(EPostStatus.Approved)] EPostStatus postStatus,
            [Values(ECommunityReadPermission.All)] ECommunityReadPermission communityReadPermission,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)] EXUserCommunityType? xUserCommunityType,
            [Values(true, false)] bool getByAuthor,
            [Values(true)] bool verify)
        {
            await GetShortValidation(postStatus, communityReadPermission, xUserCommunityType, getByAuthor, verify);
        }

        [Test, Combinatorial]
        public async ValueTask GetShortValidation_FalseCases(
            [Values(EPostStatus.Deleted, EPostStatus.Forbiden)] EPostStatus postStatus,
            [Values(ECommunityReadPermission.MembersOnly)] ECommunityReadPermission communityReadPermission,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? xUserCommunityType,
            [Values(false)] bool getByAuthor,
            [Values(false)] bool verify)
        {
            await GetShortValidation(postStatus, communityReadPermission, xUserCommunityType, getByAuthor, verify);
        }

        private async ValueTask GetShortValidation(
            EPostStatus postStatus,
            ECommunityReadPermission communityReadPermission,
            EXUserCommunityType? xUserCommunityType,
            bool getByAuthor,
            bool verify)
        {
            var validatorDto = new PostGetShortValidatorDto()
            {
                CommunityReadPermissions = communityReadPermission,
                MemberStatus = xUserCommunityType,
                PostStatus = postStatus,
                GetByAuthor = getByAuthor,
            };

            var validator = new PostGetShortValidator();
            var result = await validator.ValidateAsync(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        [Test, Combinatorial]
        public async ValueTask CreateValidation_TrueCases(
            [Values(EUserStatus.Approved)] EUserStatus authorStatus,
            [Values(ECommunityStatus.Approved)] ECommunityStatus communityStatus,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)] EXUserCommunityType? authorMembership,
            [Values(ECommunityPostCreatePermission.All)] ECommunityPostCreatePermission commPostCreatePermission,
            [Values("Post Title")] string postTitle,
            [Values("Post Content")] string postContent,
            [Values(true)] bool verify)
        {
            await CreateValidation(authorStatus, communityStatus, authorMembership, commPostCreatePermission, postTitle, postContent, verify);
        }

        [Test, Combinatorial]
        public async ValueTask CreateValidation_FalseCases(
            [Values(EUserStatus.Banned, EUserStatus.Created, EUserStatus.Deleted)] EUserStatus authorStatus,
            [Values(ECommunityStatus.Created, ECommentStatus.Deleted)] ECommunityStatus communityStatus,
            [Values(EXUserCommunityType.None, null)] EXUserCommunityType? authorMembership,
            [Values(ECommunityPostCreatePermission.MembersOnly, ECommunityPostCreatePermission.ModeratorOnly)] ECommunityPostCreatePermission commPostCreatePermission,
            [Values("")] string postTitle,
            [Values("")] string postContent,
            [Values(false)] bool verify)
        {
            await CreateValidation(authorStatus, communityStatus, authorMembership, commPostCreatePermission, postTitle, postContent, verify);
        }

        private async ValueTask CreateValidation(
            EUserStatus authorStatus,
            ECommunityStatus communityStatus,
            EXUserCommunityType? authorMembership,
            ECommunityPostCreatePermission commPostCreatePermission,
            string postTitle,
            string postContent,
            bool verify)
        {
            var validator = new PostCreateValidator();
            var validatorDto = new PostCreateValidatorDto()
            {
                AuthorStatus = authorStatus,
                CommunityStatus = communityStatus,
                CurrentUserMembership = authorMembership,
                CommunityCreatePermission = commPostCreatePermission,
                PostTitle = postTitle,
                PostContent = postContent
            };

            var result = await validator.ValidateAsync(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        [Test, Combinatorial]
        public async ValueTask UpdateValidation_TrueCases(
            [Values(EPostStatus.Approved)] EPostStatus postStatus,
            [Values(EUserStatus.Approved)] EUserStatus updUserStatus,
            [Values(EXUserCommunityType.Member, EXUserCommunityType.Moderator)] EXUserCommunityType? updMembership,
            [Values(true)] bool updUserIsAuthor,
            [Values("Post Title")] string newPostTitle,
            [Values("Post Content")] string newPostContent,
            [Values(true)] bool verify)
        {
            await UpdateValidation(postStatus, updUserStatus, updMembership, updUserIsAuthor, DateTime.UtcNow, newPostTitle, newPostContent, verify);
        }

        [Test, Combinatorial]
        public async ValueTask UpdateValidation_FalseCases(
            [Values(EPostStatus.Deleted, EPostStatus.Forbiden)] EPostStatus postStatus,
            [Values(EUserStatus.Banned, EUserStatus.Created, EUserStatus.Deleted)] EUserStatus updUserStatus,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? updMembership,
            [Values(false)] bool updUserIsAuthor,
            [Values("")] string newPostTitle,
            [Values("")] string newPostContent,
            [Values(false)] bool verify)
        {
            await UpdateValidation(postStatus, updUserStatus, updMembership, updUserIsAuthor, DateTime.UtcNow, newPostTitle, newPostContent, verify);
        }

        public async ValueTask UpdateValidation(
            EPostStatus postStatus,
            EUserStatus updUserStatus,
            EXUserCommunityType? updMembership,
            bool updUserIsAuthor,
            DateTime postCreateDate,
            string newPostTitle,
            string newPostContent,
            bool verify)
        {
            var validator = new PostUpdateValidator(TestTools.CoreOptions);
            var validatorDto = new PostUpdateValidatorDto()
            {
                CurUserStatus = updUserStatus,
                CurUserMembership = updMembership,
                CurUserIsAuthor = updUserIsAuthor,

                NewTitle = newPostTitle,
                NewContent = newPostContent,

                PostCreateDate = postCreateDate,
                PostStatus = postStatus,
            };

            var result = await validator.ValidateAsync(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        [Test, Combinatorial]
        public async ValueTask DeleteValidation_TrueCases(
            [Values(ECommunityPostDeletePermission.AuthorOnly)] ECommunityPostDeletePermission commPostDeletePerm,
            [Values(EUserStatus.Approved)] EUserStatus curUserStatus,
            [Values(true)] bool curUserIsAuthor,
            [Values(EXUserCommunityType.Member)] EXUserCommunityType? curUserMembership,
            [Values(true)] bool verify)
        {
            await DeleteValidation(commPostDeletePerm, curUserStatus, curUserIsAuthor, DateTime.UtcNow-TimeSpan.FromMinutes(5), curUserMembership, verify);
        }

        [Test, Combinatorial]
        public async ValueTask DeleteValidation_FalseCases(
            [Values(ECommunityPostDeletePermission.ModeratorOnly)] ECommunityPostDeletePermission commPostDeletePerm,
            [Values(EUserStatus.Banned, EUserStatus.Deleted, EUserStatus.Created)] EUserStatus curUserStatus,
            [Values(false)] bool curUserIsAuthor,
            [Values(null, EXUserCommunityType.None)] EXUserCommunityType? curUserMembership,
            [Values(false)] bool verify)
        {
            await DeleteValidation(commPostDeletePerm, curUserStatus, curUserIsAuthor, DateTime.UtcNow - TimeSpan.FromMinutes(5), curUserMembership, verify);
        }

        private async ValueTask DeleteValidation(
            ECommunityPostDeletePermission commPostDeletePerm,
            EUserStatus curUserStatus,
            bool curUserIsAuthor,
            DateTime postCreateDate,
            EXUserCommunityType? curUserMembership,
            bool verify)
        {
            var validator = new PostDeleteValidator();
            var validatorDto = new PostDeleteValidatorDto()
            {
                PostCreateDate = postCreateDate,
                CommPostDeletePermission = commPostDeletePerm,
                CurUserStatus = curUserStatus,
                CurUserIsAuthor = curUserIsAuthor,
                CurUserMembership = curUserMembership,
            };

            var result = await validator.ValidateAsync(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }
    }
}
