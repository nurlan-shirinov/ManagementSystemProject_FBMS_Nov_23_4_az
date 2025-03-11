﻿using AutoMapper;
using FluentValidation;
using ManagementSystem.Application.AutoMapper;
using ManagementSystem.Application.PipelineBehaviours;
using ManagementSystem.Application.Services.BackgroundServices;
using ManagementSystem.Application.Services.LoggingService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace ManagementSystem.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/app-log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ILoggerService, LoggerService>();
        //services.AddHostedService<DeleteUserBackgroundService>();

        return services;
    }
}