using MediatR;
using MF_Task.Service.DTOs;

namespace MF_Task.Service.Queries.Handlers
{
    public abstract class BaseQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    }
}