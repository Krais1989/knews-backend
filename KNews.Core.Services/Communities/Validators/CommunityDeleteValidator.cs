using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityDeleteValidatorDto
    {
        public ECommunityStatus CommunityStatus { get; private set; }
        public EUserStatus CurUserStatus { get; private set; }
        public bool CurUserIsAdmin { get; private set; }
        public bool CurUserIsOwner { get; private set; }

        public CommunityDeleteValidatorDto(
            ECommunityStatus communityStatus,
            EUserStatus curUserStatus,
            bool curUserIsAdmin,
            bool curUserIsOwner)
        {
            CommunityStatus = communityStatus;
            CurUserStatus = curUserStatus;
            CurUserIsAdmin = curUserIsAdmin;
            CurUserIsOwner = curUserIsOwner;
        }
    }

    public class CommunityDeleteValidator : AbstractValidator<CommunityDeleteValidatorDto>
    {
        public CommunityDeleteValidator()
        {
            RuleFor(dto => dto.CommunityStatus).NotEqual(ECommunityStatus.Deleted);
            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
            RuleFor(dto => dto.CurUserIsAdmin).Equal(true).When(dto => dto.CommunityStatus == ECommunityStatus.Approved);
            RuleFor(dto => dto.CurUserIsOwner).Equal(true).When(dto => dto.CommunityStatus == ECommunityStatus.Created);
        }
    }
}