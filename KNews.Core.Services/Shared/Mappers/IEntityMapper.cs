using System;
using System.Linq.Expressions;

namespace KNews.Core.Services.Shared.Mappers
{
    public interface IEntityMapper<TSource, TTarget>
    {
        TTarget Map(TSource entity);
        TSource Map(TTarget entity);

        /// <summary>
        /// Expression для генерации запроса EF Core
        /// </summary>
        Expression<Func<TSource, TTarget>> MapExpr { get; }
    }
}