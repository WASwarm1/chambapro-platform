using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Commands;

namespace ChambaPro.Platform.API.Service.Domain.Service;

public interface IServiceCommandService
{
    Task<Services> Handle(CreateServiceCommand command);
    Task<Services> Handle(UpdateServiceCommand command);
    Task Handle(DeleteServiceCommand command);
    Task<Services> Handle(CompleteServiceCommand command);
    Task<Services> Handle(ConfirmServiceCommand command);
    Task<Services> Handle(CancelServiceCommand command);
}