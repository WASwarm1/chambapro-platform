using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Queries;
using ChambaPro.Platform.API.IAM.Domain.Repositories;
using ChambaPro.Platform.API.IAM.Domain.Services;

namespace ChambaPro.Platform.API.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.UserId);
    }

    public async Task<User?> Handle(GetUserByEmailQuery query)
    {
        return await _userRepository.FindByEmailAsync(query.email);
    }

    public async Task<User?> Handle(GetUserByEmailAndTypeQuery query)
    {
        return await _userRepository.FindByEmailAndTypeAsync(
            query.Email, 
            query.UserType
        );
    }

    public async Task<IEnumerable<User>> Handle(GetAllTechniciansQuery query)
    {
        return await _userRepository.FindAllTechniciansAsync();
    }

    public async Task<IEnumerable<User>> Handle(GetTechniciansBySpecialityQuery query)
    {
        return await _userRepository.FindTechniciansBySpecialityAsync(query.Speciality);
    }
}