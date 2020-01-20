using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityGetFullValidatorDto
    {
        public bool CurUserIsAuthor { get; private set; }
        public EUserStatus? CurUserStatus { get; private set; }
        public EUserMembershipStatus? MembershipType { get; private set; }
        public ECommunityStatus CommunityStatus { get; private set; }
        public ECommunityReadPermissions CommunityReadPermission { get; private set; }

        public CommunityGetFullValidatorDto(
            bool curUserIsAuthor,
            EUserStatus? curUserStatus,
            EUserMembershipStatus? membershipType,
            ECommunityStatus communityStatus,
            ECommunityReadPermissions communityReadPermission)
        {
            CurUserIsAuthor = curUserIsAuthor;
            CurUserStatus = curUserStatus;
            MembershipType = membershipType;
            CommunityStatus = communityStatus;
            CommunityReadPermission = communityReadPermission;
        }
    }

    public class CommunityGetFullValidator : AbstractValidator<CommunityGetFullValidatorDto>
    {
        public CommunityGetFullValidator()
        {
            RuleFor(dto => dto.CurUserStatus)
                .NotNull()
                .When(dto => dto.CommunityReadPermission != ECommunityReadPermissions.All);

            RuleFor(dto => dto.MembershipType)
                .NotNull()
                .NotEqual(EUserMembershipStatus.None)
                .When(dto => dto.CommunityReadPermission == ECommunityReadPermissions.Members);

            RuleFor(dto => dto.CommunityStatus).NotEqual(ECommunityStatus.Deleted);
            
            /* Читать сообщества в статусе Created могут читать лишь авторы и модераторы комьюнити */
        }
    }

}