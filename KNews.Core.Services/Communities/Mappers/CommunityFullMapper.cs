using KNews.Core.Entities;
using KNews.Core.Services.Communities.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Linq.Expressions;

namespace KNews.Core.Services.Communities.Mappers
{
    public class CommunityFullMapper : IEntityMapper<Community, CommunityFull>
    {
        public Expression<Func<Community, CommunityFull>> MapExpr =>
            x => new CommunityFull
            {
                ID = x.ID,
                Status = x.Status,
                Title = x.Title,
                Description = x.Description,
                Rules = x.Rules,
            };

        public CommunityFull Map(Community entity)
        {
            throw new NotImplementedException();
        }

        public Community Map(CommunityFull entity)
        {
            throw new NotImplementedException();
        }
    }
}