using System.Text;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.Application.Mapping;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Infrastructure.DataBase;
using CarSharingApp.CarService.Infrastructure.Repositories;
using CarSharingApp.CarService.WebAPI.Consumers;
using CarSharingApp.CarService.WebAPI.Extensions;
using CarSharingApp.CarService.WebAPI.MiddleWares;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minio;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService;

namespace CarSharingApp.CarService.WebAPI;

public class Startup
{
    private static string MyAllowSpecificOrigins { get; set; }
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateCarStatusCommand).Assembly));
    }

    public static void ConfigureMassTransit(IServiceCollection services, ConfigurationManager config)
    {
        services.AddMassTransit(x =>
        {
            var assembly = typeof(UpdateCarStatusConsumer).Assembly;
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

    public static void ConfigureGRPC(IServiceCollection services, ConfigurationManager config)
    {
        services.AddGrpc();
        services.AddGrpcClient<User.UserClient>(options =>
            options.Address = new Uri(config["gRPC:UserConnection"]));
    }
    
    public static void ConfigureGRPC(WebApplication app)
    {
        app.MapGrpcService<gRPC.Services.CarService>();
    }
    
    public static void ConfigureRedis(IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));
        services.AddSingleton<ICacheService ,CacheService>();
    }
    
    public static void ConfigureCors(IServiceCollection services)
    {
        MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });
    }

    public static void ConfigureCors(WebApplication app)
    {
        app.UseCors(MyAllowSpecificOrigins);
    }
    
    public static void UseMigrations(WebApplication app)
    {
        var helperContext = app.Services.GetService<HelperContext>();
        var dbContext = app.Services.GetService<CarsContext>();
        dbContext.Database.Migrate();
        helperContext.Database.Migrate();
    }

}