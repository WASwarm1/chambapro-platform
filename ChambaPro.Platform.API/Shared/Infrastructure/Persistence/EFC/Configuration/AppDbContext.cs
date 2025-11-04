using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Chambapro.Platform.API.User.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Reserve> Reserves { get; set; } = null!;
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public DbSet<Reviews> Reviews { get; set; } = null!;
    public DbSet<Services> Services { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reserve>().HasKey(r => r.Id);
        modelBuilder.Entity<Reserve>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Reserve>().Property(r => r.Date).IsRequired();
        modelBuilder.Entity<Reserve>().Property(r => r.Time).IsRequired();
        modelBuilder.Entity<Reserve>().Property(r => r.Description).HasMaxLength(500);
        modelBuilder.Entity<Reserve>().Property(r => r.ClientId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Reserve>().Property(r => r.CategoryId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Reserve>().Property(r => r.TechnicianId).HasMaxLength(100);
        modelBuilder.Entity<Reserve>().Property(r => r.Status).IsRequired();

        modelBuilder.Entity<UserProfile>().HasKey(up => up.Id);
        modelBuilder.Entity<UserProfile>().Property(up => up.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<UserProfile>().Property(up => up.UserId).IsRequired();
        modelBuilder.Entity<UserProfile>().Property(up => up.FullName).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<UserProfile>().Property(up => up.PhoneNumber).IsRequired().HasMaxLength(20);
        modelBuilder.Entity<UserProfile>().Property(up => up.Address).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<UserProfile>().Property(up => up.Bio).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<UserProfile>().Property(up => up.ProfilePictureUrl).HasMaxLength(500);
        modelBuilder.Entity<UserProfile>().Property(up => up.CreatedAt).IsRequired();
        modelBuilder.Entity<UserProfile>().Property(up => up.UpdatedAt);
        
        modelBuilder.Entity<Reviews>().HasKey(r => r.Id);
        modelBuilder.Entity<Reviews>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Reviews>().Property(r => r.TechnicianId).IsRequired();
        modelBuilder.Entity<Reviews>().Property(r => r.ClientId).IsRequired();
        modelBuilder.Entity<Reviews>().Property(r => r.Rating).IsRequired();
        modelBuilder.Entity<Reviews>().Property(r => r.Comment).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<Reviews>().Property(r => r.CreationDate).IsRequired();
        
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Phone).IsRequired().HasMaxLength(20);
        modelBuilder.Entity<User>().Property(u => u.Avatar).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<User>().Property(u => u.Type).IsRequired().HasConversion<string>().HasMaxLength(20);
        modelBuilder.Entity<User>().Property(u => u.CreatedAt).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Speciality).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Description).HasMaxLength(1000);
        modelBuilder.Entity<User>().Property(u => u.Experience).HasMaxLength(500);
        modelBuilder.Entity<User>().Property(u => u.Rating).HasPrecision(3, 2);
        modelBuilder.Entity<User>().Property(u => u.HourlyRate).HasPrecision(10, 2);
        
        modelBuilder.Entity<Services>().HasKey(s => s.Id);
        modelBuilder.Entity<Services>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Services>().Property(s => s.ClientId).IsRequired();
        modelBuilder.Entity<Services>().Property(s => s.TechnicianId).IsRequired();
        modelBuilder.Entity<Services>().Property(s => s.Date).IsRequired();
        modelBuilder.Entity<Services>().Property(s => s.Time).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Services>().Property(s => s.Description).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<Services>().Property(s => s.Category).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Services>().Property(s => s.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
        modelBuilder.Entity<Services>().Property(s => s.Cost).IsRequired().HasPrecision(10, 2);
        modelBuilder.Entity<Services>().Property(s => s.Duration).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Services>().Property(s => s.Address).IsRequired().HasMaxLength(500);
        
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.UseSnakeCaseNamingConvention();
    }
}