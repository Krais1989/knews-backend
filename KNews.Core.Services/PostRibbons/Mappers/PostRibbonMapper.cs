using KNews.Core.Entities;
using KNews.Core.Services.PostRibbons.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNews.Core.Services.PostRibbons.Mappers
{
    public class PostRibbonMapper : IEntityMapper<Post, PostRibbon>
    {
        public Expression<Func<Post, PostRibbon>> MapExpr =>
            x => new PostRibbon
            {
                ID = x.ID,
                Title = x.Title,
                ShortContent = x.ShortContent,
                CreateDate = x.CreateDate,
                CommunityID = x.CommunityID,
                CommunityTitle = x.Community.Title,
                AuthorID = x.AuthorID,
                AuthorTitle = $"{ x.Author.FirstName} { x.Author.LastName}",
            };

        public PostRibbon Map(Post entity)
        {
            throw new NotImplementedException();
        }

        public Post Map(PostRibbon entity)
        {
            throw new NotImplementedException();
        }
    }
}
