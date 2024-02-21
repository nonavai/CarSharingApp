using System.Text;
using CarSharingApp.DealService.API.Extensions;
using CarSharingApp.DealService.API.MiddleWares;
using CarSharingApp.DealService.BusinessLogic.CommandHandlers.AnswerCommandHandlers;
using CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;
using CarSharingApp.DealService.BusinessLogic.CommandHandlers.FeedbackCommandHandlers;
using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Mapping;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;
using CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;
using CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;
using CarSharingApp.DealService.BusinessLogic.QueryHandlers.AnswerQueryHandlers;
using CarSharingApp.DealService.BusinessLogic.QueryHandlers.DealQueryHandlers;
using CarSharingApp.DealService.BusinessLogic.QueryHandlers.FeedbackQueryHandlers;
using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.DataBase.AdditionalDB;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.DataAccess.Repositories.Implementations;
using Hangfire;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace CarSharingApp.DealService.API;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        //deal commands
        services.AddTransient<IRequestHandler<CreateDealCommand, DealDto>, CreateDealHandler>();
        services.AddTransient<IRequestHandler<CancelDealCommand, DealDto>, CancelDealHandler>();
        services.AddTransient<IRequestHandler<ConfirmDealCommand, DealDto>, ConfirmDealHandler>();
        services.AddTransient<IRequestHandler<CompleteDealCommand, DealDto>, CompleteDealHandler>();
        //feedback commands
        services.AddTransient<IRequestHandler<CreateFeedbackCommand, FeedbackDto>, CreateFeedbackHandler>();
        services.AddTransient<IRequestHandler<UpdateFeedbackCommand, FeedbackDto>, UpdateFeedbackHandler>();
        services.AddTransient<IRequestHandler<DeleteFeedbackCommand, FeedbackDto>, DeleteFeedbackHandler>();
        //answer commands
        services.AddTransient<IRequestHandler<CreateAnswerCommand, AnswerDto>, CreateAnswerHandler>();
        services.AddTransient<IRequestHandler<UpdateAnswerCommand, AnswerDto>, UpdateAnswerHandler>();
        services.AddTransient<IRequestHandler<DeleteAnswerCommand, AnswerDto>, DeleteAnswerHandler>();
        //deal queries
        services.AddTransient<IRequestHandler<GetDealsByCarQuery, IEnumerable<DealDto>>, GetDealsByCarHandler>();
        services.AddTransient<IRequestHandler<GetDealsByUserQuery, IEnumerable<DealDto>>, GetDealsByUserHandler>();
        //answer queries
        services.AddTransient<IRequestHandler<GetAnswersByFeedbackQuery, IEnumerable<AnswerDto>>, GetAnswersByFeedbackHandler>();
        // feedback queries
        services.AddTransient<IRequestHandler<GetFeedbackByDealQueries, IEnumerable<FeedbackDto>>, GetFeedbackByDealHandler>();
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
        services.AddTransient<IDealRepository, DealRepository>();
        services.AddTransient<IAnswerRepository, AnswerRepository>();
        services.AddTransient<IFeedBackRepository, FeedbackRepository>();
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
}