using MediatR;

namespace KNews.Core.Services
{
    public interface IRepositoryModifyRequest<T> : IRequest<T>
    {
        bool SaveChanges { get; set; }
    }
}
