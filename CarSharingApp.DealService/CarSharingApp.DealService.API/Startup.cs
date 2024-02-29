using System.Text;
using CarService;
using CarSharingApp.DealService.API.Extensions;
using CarSharingApp.DealService.API.MiddleWares;
using CarSharingApp.DealService.BusinessLogic.Caching;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Mapping;
using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.DataBase.AdditionalDB;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.DataAccess.Repositories.Implementations;
using Hangfire;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService;


namespace CarSharingApp.DealService.API;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDealCommand).Assembly));
    }

    public static void ConfigureMassTransit(IServiceCollection services, ConfigurationManager config)
    {
        services.AddMassTransit(x =>
        {
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
            

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
        
        services.AddMassTransitHostedService();
    }
    
    public static void ConfigureHangfire(IApplicationBuilder app, ConfigurationManager config)
    {
        GlobalConfiguration.Configuration.UseSqlServerStorage(config.GetConnectionString("HelperDB"));
    
        app.UseHangfireServer();
        app.UseHangfireDashboard();
    }
    
    public static void ConfigureRepository(IServiceCollection services)
    {
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IFeedBackRepository, FeedbackRepository>();
    }
    
    public static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationExtension>();
    }
    
    public static void ConfigureDataBase(IServiceCollection services, ConfigurationManager config)
    {
        var connection = config.GetConnectionString("Database");
        var name = config.GetConnectionString("DatabaseName");
        var helperDb = config.GetConnectionString("HelperDB");
        
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connection + name));
        
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(connection + name);
        });
        
        services.AddSingleton<MongoContext>();
        
        services.AddDbContext<HelperContext>(options => options.UseSqlServer(helperDb));

        services.AddHangfire(configuration =>
        {
            configuration.UseSqlServerStorage(helperDb);
        });

        services.AddHangfireServer();
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
    
    public static void ConfigureGRPC(IServiceCollection services, ConfigurationManager config)
    {
        services.AddGrpc();
        services.AddGrpcClient<User.UserClient>(options =>
            options.Address = new Uri(config["gRPC:UserConnection"]));
        services.AddGrpcClient<Car.CarClient>(options =>
            options.Address = new Uri(config["gRPC:CarConnection"]));
    }

    public static void ConfigureRedis(IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));
        services.AddSingleton<ICacheService ,CacheService>();
    }
}