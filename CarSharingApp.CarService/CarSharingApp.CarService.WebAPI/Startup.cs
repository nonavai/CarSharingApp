﻿using System.Text;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;
using CarSharingApp.CarService.Application.CommandHandlers.CarStateHandlers;
using CarSharingApp.CarService.Application.CommandHandlers.CommentCommandHandlers;
using CarSharingApp.CarService.Application.CommandHandlers.ImageCommandHandlers;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Mapping;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;
using CarSharingApp.CarService.Application.QueryHandlers.CommentQueryHandlers;
using CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Infrastructure.DataBase;
using CarSharingApp.CarService.Infrastructure.Repositories;
using CarSharingApp.CarService.WebAPI.Consumers;
using CarSharingApp.CarService.WebAPI.Extensions;
using CarSharingApp.CarService.WebAPI.MiddleWares;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minio;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarSharingApp.CarService.WebAPI;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        //Car CommandHandlers
        services.AddTransient<IRequestHandler<CreateCarCommand, CarDto>, CreateCarHandler>();
        services.AddTransient<IRequestHandler<UpdateCarCommand, CarDto>, UpdateCarHandler>();
        services.AddTransient<IRequestHandler<DeleteCarCommand, CarDto>, DeleteCarHandler>();
        services.AddTransient<IRequestHandler<DeleteCarByUserCommand, IEnumerable<CarDto>>, DeleteCarByUserHandler>();
        //Comment CommandHandlers
        services.AddTransient<IRequestHandler<UpdateCommentCommand, CommentDto>, UpdateCommentHandler>();
        services.AddTransient<IRequestHandler<CreateCommentCommand, CommentDto>, CreateCommentHandler>();
        services.AddTransient<IRequestHandler<DeleteCommentCommand, CommentDto>, DeleteCommentHandler>();
        //Image CommandHandlers
        services.AddTransient<IRequestHandler<CreateImageCommand, ImageDto>, CreateImageHandler>();
        services.AddTransient<IRequestHandler<UpdateImagePriorityCommand, ImageDto>, UpdateImagePriorityHandler>();
        services.AddTransient<IRequestHandler<DeleteImageCommand, ImageDto>, DeleteImageHandler>();
        //CarState CommandHandlers
        services.AddTransient<IRequestHandler<UpdateCarStatusCommand, CarStateDto>, UpdateCarStatusHandler>();
        services.AddTransient<IRequestHandler<UpdateCarLocationCommand, CarStateDto>, UpdateCarLocationHandler>();
        //Car QueryHandlers
        services.AddTransient<IRequestHandler<GetCarQuery, CarFullDto>, GetCarHandler>();
        services.AddTransient<IRequestHandler<GetCarsByParamsQuery, IEnumerable<CarWithImageDto>>, GetCarsByParamsHandler>();
        services.AddTransient<IRequestHandler<GetCarByUserQuery, IEnumerable<CarDto>>, GetCarByUserHandler>();
        //Comment QueryHandlers
        services.AddTransient<IRequestHandler<GetCommentsByCarQuery, IEnumerable<CommentDto>>, GetCommentsByCarHandler>();
        //Image QueryHandlers
        services.AddTransient<IRequestHandler<GetImagesByCarQuery, IEnumerable<ImageFullDto>>, GetImagesByCarHandler>();
    }

    public static void ConfigureMassTransit(IServiceCollection services, ConfigurationManager config)
    {
        services.AddMassTransit(x =>
        {
            var assembly = typeof(DeleteCarConsumer).Assembly;
            var host = config["RabbitMQ:Host"];
            var virtualHost = config["RabbitMQ:VirtualHost"];
            var username = config["RabbitMQ:Username"];
            var password = config["RabbitMQ:Password"];

            x.AddEntityFrameworkOutbox<HelperContext>(o =>
            {
                o.UseSqlServer();

                o.QueryDelay = TimeSpan.FromSeconds(120);

                o.UseBusOutbox();
            });

            x.AddConsumers(assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(context);

            });
        });
    }
    
    public static void InitializeMinio(IServiceCollection services, ConfigurationManager config)
    {
        var endPoint = config["MinIO-Settings:EndPoint"];
        var accessKey = config["MinIO-Settings:AccessKey"];
        var secretKey = config["MinIO-Settings:SecretKey"];
        
        services.AddMinio(configureClient => configureClient
            .WithEndpoint(endPoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(false)
            .Build());
    }
    
    public static void ConfigureRepository(IServiceCollection services)
    {
        services.AddTransient<IMinioRepository, MinioRepository>();
        services.AddTransient<ICarImageRepository, CarImageRepository>();
        services.AddTransient<ICarRepository, CarRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICarStateRepository, CarStateRepository>();
    }
    
    public static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationExtension>();
    }
    
    public static void ConfigureDataBase(IServiceCollection services, ConfigurationManager config)
    {
        var connectionString = config.GetConnectionString("DataBase");
        var helperConnectionString = config.GetConnectionString("HelperDB");
        services.AddDbContext<CarsContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<HelperContext>(options =>
            options.UseSqlServer(helperConnectionString));
    }
    
    public static void ConfigureMiddlewares(WebApplication app)
    {
        app.UseMiddleware<ExceptionAndLoggingMiddleware>();
    }
    
    public static void ConfigureAuth(IServiceCollection services, ConfigurationManager config)
    {
        services.AddAuthentication(x =>
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
    }
    
    public static void ConfigureRedis(IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));
        services.AddSingleton<ICacheService ,CacheService>();
    }
}