using KNews.Core.Entities;
using KNews.Core.Services.Comments.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNews.Core.Services.Comments.Mappers
{
    public class CommentForRibbonMapper : IEntityMapper<Comment, CommentForRibbon>
    {
        public Expression<Func<Comment, CommentForRibbon>> MapExpr =>
            x => new CommentForRibbon
            {
                ID = x.ID,
                Status = x.Status,
                PostID = x.PostID,
                AuthorID = x.AuthorID,
                ShortContent = x.ShortContent,
                CreateDate = x.CreateDate,
                AuthorTitle = $"{x.Author.FirstName} {x.Author.LastName}",
                PostTitle = x.Post.Title
            };

        public CommentForRibbon Map(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment Map(CommentForRibbon entity)
        {
            throw new NotImplementedException();
        }
    }
}
