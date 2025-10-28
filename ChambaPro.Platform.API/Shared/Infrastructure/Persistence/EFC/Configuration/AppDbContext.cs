using Chambapro_backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<Reserve> Reserves { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        
        
        base.OnModelCreating(builder);

        builder.Entity<Reserve>().HasKey(r => r.Id);
        builder.Entity<Reserve>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Reserve>().Property(r => r.Date).IsRequired();
        builder.Entity<Reserve>().Property(r => r.Time).IsRequired();
        builder.Entity<Reserve>().Property(r => r.Description).HasMaxLength(500);
        builder.Entity<Reserve>().Property(r => r.ClientId).IsRequired().HasMaxLength(100);
        builder.Entity<Reserve>().Property(r => r.CategoryId).IsRequired().HasMaxLength(100);
        builder.Entity<Reserve>().Property(r => r.TechnicianId).HasMaxLength(100);
        builder.Entity<Reserve>().Property(r => r.Status).IsRequired();

        builder.UseSnakeCaseNamingConvention();
    }
}