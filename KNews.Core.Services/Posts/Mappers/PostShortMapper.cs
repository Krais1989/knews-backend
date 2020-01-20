using KNews.Core.Entities;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Linq.Expressions;

namespace KNews.Core.Services.Posts.Mappers
{
    public class PostShortMapper : IEntityMapper<Post, PostShort>
    {
        public Expression<Func<Post, PostShort>> MapExpr =>
            x => new PostShort
            {
                ID = x.ID,
                Title = x.Title,
                Content = x.Content,
                ShortContent = x.ShortContent,
                AuthorTitle = $"{ x.Author.FirstName} { x.Author.LastName}",
                CommunityTitle = x.Community.Title,
                CreateDate = x.CreateDate
            };

        public PostShort Map(Post entity)
        {
            throw new NotImplementedException();
        }

        public Post Map(PostShort entity)
        {
            throw new NotImplementedException();
        }
    }
}
