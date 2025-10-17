using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.IAM.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(int id);
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByEmailAndTypeAsync(string email, string type);
    Task<IEnumerable<User>> FindAllTechniciansAsync();
    Task<IEnumerable<User>> FindTechniciansBySpecialityAsync(string speciality);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
    void Update(User user);
}