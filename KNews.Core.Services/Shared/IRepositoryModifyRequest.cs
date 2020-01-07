using MediatR;

namespace KNews.Core.Services.Shared
{
    public interface IRepositoryModifyRequest<T> : IRequest<T>
    {
        bool SaveChanges { get; set; }
    }
}