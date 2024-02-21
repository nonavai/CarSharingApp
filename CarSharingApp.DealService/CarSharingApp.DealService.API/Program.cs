using CarSharingApp.DealService.API;
using CarSharingApp.DealService.BusinessLogic.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAnswerValidation>(); 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSignalR();

Startup.ConfigureDataBase(builder.Services, config);
Startup.ConfigureSwagger(builder.Services);
Startup.ConfigureAuth(builder.Services, config);
Startup.ConfigureRepository(builder.Services);
Startup.ConfigureServices(builder.Services);
Startup.ConfigureMassTransit(builder.Services, config);

var app = builder.Build();

//Startup.ConfigureMiddlewares(app);
Startup.ConfigureHangfire(app, config);

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