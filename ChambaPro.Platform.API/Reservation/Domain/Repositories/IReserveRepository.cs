using Chambapro_backend.Shared.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.Reservation.Domain.Repositories;

public interface IReserveRepository : IBaseRepository<Reserve>
{
    Task<IEnumerable<Reserve>> FindByClientIdAsync(string clientId);
    Task<IEnumerable<Reserve>> FindByTechnicianIdAsync(string technicianId);
}