using KNews.Core.Entities;
using KNews.Core.Services.Communities.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Linq.Expressions;

namespace KNews.Core.Services.Communities.Mappers
{
    public class CommunityShortMapper : IEntityMapper<Community, CommunityShort>
    {
        public Expression<Func<Community, CommunityShort>> MapExpr =>
            x => new CommunityShort
            {
                ID = x.ID,
                Status = x.Status,
                Title = x.Title,
                Description = x.Description,
                Rules = x.Rules,
            };

        public CommunityShort Map(Community entity)
        {
            throw new NotImplementedException();
        }

        public Community Map(CommunityShort entity)
        {
            throw new NotImplementedException();
        }
    }
}