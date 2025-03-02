using AutoMapper;
using FluentValidation;
using ManagementSystem.Application.AutoMapper;
using ManagementSystem.Application.PipelineBehaviours;
using ManagementSystem.Application.Services.BackgroundServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManagementSystem.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddHostedService<DeleteUserBackgroundService>();

        return services;
    }
}