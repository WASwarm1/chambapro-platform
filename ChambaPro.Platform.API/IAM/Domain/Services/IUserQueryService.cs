using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Queries;

namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByEmailQuery query);
    Task<User?> Handle(GetUserByEmailAndTypeQuery query);
    Task<IEnumerable<User>> Handle(GetAllTechniciansQuery query);
    Task<IEnumerable<User>> Handle(GetTechniciansBySpecialityQuery query);
}