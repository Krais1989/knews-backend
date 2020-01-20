using KNews.Core.Entities;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNews.Core.Services.Posts.Mappers
{
    public class PostFullMapper : IEntityMapper<Post, PostFull>
    {
        public Expression<Func<Post, PostFull>> MapExpr =>
            x => new PostFull
            {
                ID = x.ID,
                Title = x.Title,
                Content = x.Content,
                ShortContent = x.ShortContent,
                AuthorID = x.AuthorID,
                CommunityID = x.CommunityID,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                DeleteDate = x.DeleteDate,
                Status = x.Status,
                AuthorTitle = $"{ x.Author.FirstName} { x.Author.LastName}",
                CommunityTitle = x.Community.Title,
            };

        public PostFull Map(Post entity)
        {
            throw new NotImplementedException();
        }

        public Post Map(PostFull entity)
        {
            throw new NotImplementedException();
        }
    }
}
