using MediatR;

namespace MF_Task.Service.Queries.Handlers
{
    public abstract class BaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
         where TQuery : IRequest<TResponse>
    {
        public virtual async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(query, cancellationToken);
        }
        protected abstract Task<TResponse> ExecuteQueryAsync(TQuery query, CancellationToken cancellationToken);
    }
}