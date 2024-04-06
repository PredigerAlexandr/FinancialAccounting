using Application.Interfaces;
using MediatR;

namespace Application.Services;

public class ServiceBase
{
    protected readonly IMediator Mediator;

    public ServiceBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}