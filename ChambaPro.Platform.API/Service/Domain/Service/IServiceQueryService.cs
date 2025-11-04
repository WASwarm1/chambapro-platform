using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;

namespace ChambaPro.Platform.API.Service.Domain.Service;

public interface IServiceQueryService
{
    Task<Services?> Handle(GetServiceByIdQuery query);
    Task<IEnumerable<Services>> Handle(GetAllServicesQuery query);
    Task<IEnumerable<Services>> Handle(GetServicesByClientIdQuery query);
    Task<IEnumerable<Services>> Handle(GetServicesByTechnicianIdQuery query);
    Task<IEnumerable<ServicesAudit>> GetServiceAuditTrailAsync(int serviceId);
}
