using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNews.Core.Services.Mappers
{
    public interface IEntityMapper<TSource, TTarget>
    {
        TTarget Map(TSource entity);
    }
}
