using MediatR;

namespace MF_Task.Service.Queries
{
    public abstract class BaseQuery<T> : IRequest<T>
    {
    }
}
