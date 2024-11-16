using MediatR;
using MF_Task.Service.DTOs;

namespace MF_Task.Service.Commands.Handlers
{
    public abstract class BaseCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponseDTO<object>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected TResponse CreateSuccessResponse(string message)
        {
            return (TResponse)Activator.CreateInstance(typeof(TResponse), true, message, null);
        }

        protected TResponse CreateFailureResponse(string message, List<string> errors = null)
        {
            return (TResponse)Activator.CreateInstance(typeof(TResponse), false, message, errors);
        }
    }
}
