using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;

namespace ChambaPro.Platform.API.Service.Domain.Repository;

public interface IServiceRepository
{
    Task<Services?> GetByIdAsync(int id);
    Task<IEnumerable<Services>> GetAllAsync(GetAllServicesQuery query);
    Task<IEnumerable<Services>> GetByClientIdAsync(GetServicesByClientIdQuery query);
    Task<IEnumerable<Services>> GetByTechnicianIdAsync(GetServicesByTechnicianIdQuery query);
    Task<Services> AddAsync(Services service);
    Task<Services> UpdateAsync(Services service);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    
    // Audit methods
    Task AddAuditAsync(ServicesAudit audit);
    Task<IEnumerable<ServicesAudit>> GetAuditTrailAsync(int serviceId);
}