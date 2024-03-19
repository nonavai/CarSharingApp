using CarSharingApp.Identity.API;
using CarSharingApp.Identity.BusinessLogic.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
Startup.OptionsConfigure(builder.Services, config);
Startup.ConfigureIdentity(builder.Services);
Startup.ConfigureDataBase(builder.Services, config);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(); 
builder.Services.AddFluentValidationAutoValidation();
Startup.ConfigureCors(builder.Services);
Startup.ConfigureSwagger(builder.Services);
Startup.ConfigureAuth(builder.Services, config);
Startup.ConfigureRepository(builder.Services);
Startup.ConfigureServices(builder.Services);
Startup.InitializeRoles(builder.Services).Wait();
Startup.ConfigureMassTransit(builder.Services, config);

var app = builder.Build();
Startup.ConfigureCors(app);
Startup.ConfigureMiddlewares(app);
Startup.ConfigureHangfire(app, config);
Startup.ScheduleLicenceCheck(builder.Services);
Startup.ConfigureGRPC(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();