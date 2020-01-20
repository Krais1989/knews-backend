using KNews.Core.Entities;
using KNews.Core.Services.Comments.Entities;
using KNews.Core.Services.Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNews.Core.Services.Comments.Mappers
{
    public class CommentForPostMapper : IEntityMapper<Comment, CommentForPost>
    {
        public Expression<Func<Comment, CommentForPost>> MapExpr =>
            x => new CommentForPost
            {
                ID = x.ID,
                Status = x.Status,
                PostID = x.PostID,
                AuthorID = x.AuthorID,
                Content = x.Content,
                ShortContent = x.ShortContent,
                ParentCommentID = x.ParentCommentID,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                DeleteDate = x.DeleteDate,
                AuthorTitle = $"{x.Author.FirstName} {x.Author.LastName}"
            };

    public CommentForPost Map(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment Map(CommentForPost entity)
        {
            throw new NotImplementedException();
        }
    }
}
