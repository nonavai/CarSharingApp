using System.Text;
using CarSharingApp.Identity.BusinessLogic.Mapping;
using CarSharingApp.Identity.API.Extensions;
using CarSharingApp.Identity.API.Middlewares;
using CarSharingApp.Identity.BusinessLogic.Services;
using CarSharingApp.Identity.BusinessLogic.Services.Implementations;
using CarSharingApp.Identity.DataAccess.DbContext;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.DataAccess.Repositories.Implementation;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Options;
using Hangfire;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using Google.Protobuf;

namespace CarSharingApp.Identity.API;

public class Startup
{
    private static string MyAllowSpecificOrigins { get; set; }
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfileApi));
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserManageService, UserManageService>();
        services.AddTransient<IRolesService, RolesService>();
        services.AddGrpc();
    }
    
    public static void ConfigureRepository(IServiceCollection services)
    {
        services.AddTransient<IUserInfoRepository, UserInfoRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
    
    public static void ConfigureHangfire(IApplicationBuilder app, ConfigurationManager config)
    {
        GlobalConfiguration.Configuration.UseSqlServerStorage(config.GetConnectionString("HelperDB"));
    
        app.UseHangfireServer();
        app.UseHangfireDashboard();
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
        services.AddDbContext<CarSharingContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<HelperContext>(options =>
            options.UseSqlServer(helperConnectionString));

        services.AddHangfire(configuration =>
        {
            configuration.UseSqlServerStorage(helperConnectionString);
        });

        services.AddHangfireServer();
    }

    public static void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<CarSharingContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();
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
    
    public static async Task InitializeRoles(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            await EnsureRoleCreated(roleManager, RoleNames.Lender);
            await EnsureRoleCreated(roleManager, RoleNames.Admin);
            await EnsureRoleCreated(roleManager, RoleNames.Borrower);
        }
    }
    
    private static async Task EnsureRoleCreated(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
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

    public static void ScheduleLicenceCheck(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<IUserManageService>();

            RecurringJob.AddOrUpdate(() => userManager.DeletingExpiredLicenceAsync(), Cron.Daily);
        }
    }

    public static void OptionsConfigure(IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<JwtOptions>(config.GetSection("JwtSettings"));
    }

    public static void ConfigureGRPC(WebApplication app)
    {
        app.MapGrpcService<gRPC.Services.UserService>();
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
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var helperContext = serviceProvider.GetService<HelperContext>();
            var dbContext = serviceProvider.GetService<CarSharingContext>();
            dbContext.Database.Migrate();
            helperContext.Database.Migrate();
        }
    }
}