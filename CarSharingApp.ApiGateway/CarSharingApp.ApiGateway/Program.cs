using CarSharingApp.ApiGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureCors(builder.Services);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseSwaggerForOcelotUI(options =>
    options.PathToSwaggerGenerator = "/swagger/docs");
await app.UseOcelot();
Startup.ConfigureApp(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.Run();