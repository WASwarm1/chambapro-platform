using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Commands;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Service.Domain.Service;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.Service.Application.CommandServices;

public class ServiceCommandService(IServiceRepository repository, IUnitOfWork unitOfWork) 
    : IServiceCommandService
{
    public Task<Services> Handle(CreateServiceCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Services> Handle(UpdateServiceCommand command)
    {
        throw new NotImplementedException();
    }

    public Task Handle(DeleteServiceCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Services> Handle(CompleteServiceCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Services> Handle(ConfirmServiceCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Services> Handle(CancelServiceCommand command)
    {
        throw new NotImplementedException();
    }
}