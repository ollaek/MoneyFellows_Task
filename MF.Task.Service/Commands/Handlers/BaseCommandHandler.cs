using MediatR;

namespace MF_Task.Service.Commands.Handlers
{
    public abstract class BaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
    {
        public virtual async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(command, cancellationToken);
        }
        protected abstract Task<TResponse> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
    }
}
