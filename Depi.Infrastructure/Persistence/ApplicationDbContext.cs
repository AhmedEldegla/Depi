
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Entities.Wallets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid,
    UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
        });
        
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
        });
        
        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        
        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        
        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        
        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("UserTokens");
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}