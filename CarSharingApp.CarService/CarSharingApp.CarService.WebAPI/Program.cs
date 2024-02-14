using CarSharingApp.CarService.Application.Validators;
using CarSharingApp.CarService.WebAPI;
using CarSharingApp.CarService.WebAPI.Hubs;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCarValidation>(); 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSignalR();


Startup.ConfigureDataBase(builder.Services, config);
Startup.ConfigureSwagger(builder.Services);
Startup.ConfigureAuth(builder.Services, config);
Startup.ConfigureRepository(builder.Services);
Startup.ConfigureServices(builder.Services);
Startup.InitializeMinio(builder.Services, config);
Startup.ConfigureMassTransit(builder.Services, config);

var app = builder.Build();

Startup.ConfigureMiddlewares(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CarStateHub>("/car-state");

app.Run();