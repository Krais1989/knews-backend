namespace KNews.Core.Services.Shared.Mappers
{
    public interface IEntityMapper<TSource, TTarget>
    {
        TTarget Map(TSource entity);
        TSource Map(TTarget entity);
    }
}