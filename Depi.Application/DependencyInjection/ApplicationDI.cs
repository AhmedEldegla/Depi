using DEPI.Application.Behaviors;
using DEPI.Application.Interfaces;
using DEPI.Application.MappingProfiles;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DEPI.Application.DependencyInjection;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationDI).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProposalsMappingProfile>();
            cfg.AddProfile<ProjectsMappingProfile>();
            cfg.AddProfile<ProfilesMappingProfile>();
            cfg.AddProfile<ContractsMappingProfile>();
            cfg.AddProfile<WalletsMappingProfile>();
            cfg.AddProfile<MessagingMappingProfile>();
            cfg.AddProfile<ReviewsMappingProfile>();
            cfg.AddProfile<MediaMappingProfile>();
            cfg.AddProfile<ExtendedMappingProfile>();
        }, assembly);

        return services;
    }
}