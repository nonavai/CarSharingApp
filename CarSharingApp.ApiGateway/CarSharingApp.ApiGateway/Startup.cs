using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace CarSharingApp.ApiGateway;

public static class Startup
{
    private static string MyAllowSpecificOrigins { get; set; }
    
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

    public static void ConfigureApp(WebApplication app)
    {
        app.UseRouting();
        app.UseSwaggerForOcelotUI(options =>
            options.PathToSwaggerGenerator = "/swagger/docs");
        app.UseCors(MyAllowSpecificOrigins);
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRouting();
    }
}