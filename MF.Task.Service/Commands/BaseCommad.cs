﻿using MediatR;

namespace MF_Task.Service.Commands
{
    public abstract class BaseCommand<TResponse> : IRequest<TResponse>
    {
        
    }
}
