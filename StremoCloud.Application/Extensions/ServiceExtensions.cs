﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StremoCloud.Application.Features.Behaviours;
using StremoCloud.Infrastructure.Data;
using System.Reflection;
using StremoCloud.Application.Services;
using StremoCloud.Shared.Helpers;
using StremoCloud.Application.Features.Command.Create;
using StremoCloud.Domain.Interface;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.AspNetCore.Builder;
using StremoCloud.Domain.Entities;
//using StremoCloud.Infrastructure.Repositories;
using CloudinaryDotNet;

namespace StremoCloud.Application.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ISecurityHelper, SecurityHelper>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        //services.AddScoped<IProfileService, ProfileService>();
        //services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ITokenHelper, TokenHelper>();
        services.AddScoped<IDataContext>(x =>
        {
            var client = new MongoClient(configuration["MongoDbOptions:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbOptions:DatabaseName"]);
            return new DataContext(database);
        });

        services.AddScoped<IStremoUnitOfWork, StremoUnitOfWork>();
        services.AddValidatorsFromAssemblyContaining<ValidateOtpCommandValidator>();
        services.AddScoped<IOtpService, OtpService>();
    }

    public static void DatabaseSeederExtension(this IApplicationBuilder app, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IStremoUnitOfWork>();
        var users = unitOfWork.Repository<User>().GetList();
        if (!users.Any())
        {
            var user = new User
            {
                FirstName = configuration["DefaultUser:FirstName"],
                LastName = configuration["DefaultUser:LastName"],
                PhoneNumber = configuration["DefaultUser:PhoneNumber"],
                Email = configuration["DefaultUser:Email"],
                Address = configuration["DefaultUser:Address"],
                Password = configuration["DefaultUser:Password"].GenerateHash()
            };

            unitOfWork.Repository<User>().CreateAsync(user).Wait();
        }

    }
}
