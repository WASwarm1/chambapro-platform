using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Service.Domain.Service;

namespace ChambaPro.Platform.API.Service.Application.QueryServices;

public class ServiceQueryService(IServiceRepository repository) : IServiceQueryService
{
    public Task<Services?> Handle(GetServiceByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Services>> Handle(GetAllServicesQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Services>> Handle(GetServicesByClientIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Services>> Handle(GetServicesByTechnicianIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ServicesAudit>> GetServiceAuditTrailAsync(int serviceId)
    {
        throw new NotImplementedException();
    }
}