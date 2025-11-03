
using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Repositories;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindByEmailAndTypeAsync(string email, string type)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => 
                u.Email == email && 
                u.Type.ToString().ToLower() == type.ToLower());
    }

    public async Task<IEnumerable<User>> FindAllTechniciansAsync()
    {
        return await Context.Set<User>()
            .Where(u => u.Type == Domain.Model.ValueObjects.UserType.Technician)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> FindTechniciansBySpecialityAsync(string speciality)
    {
        return await Context.Set<User>()
            .Where(u => 
                u.Type == Domain.Model.ValueObjects.UserType.Technician &&
                u.Speciality == speciality)
            .ToListAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await Context.Set<User>().AddAsync(user);
    }

    public void Update(User user)
    {
        Context.Set<User>().Update(user);
    }
}