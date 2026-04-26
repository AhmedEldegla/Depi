using Depi.Application.Repositories.Common;
using Depi.Application.Repositories.Identity;
using DEPI.Application.Repositories.Projects;
using DEPI.Application.Repositories.Proposals;
using DEPI.Domain.Entities.Identity;
using DEPI.Infrastructure.Persistence;
using DEPI.Infrastructure.Persistence.Repositories;
using DEPI.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DEPI.Infrastructure.DependencyInjection;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<UserManager<User>>();
        services.AddScoped<RoleManager<Role>>();
        services.AddScoped<SignInManager<User>>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}