using Microsoft.EntityFrameworkCore;
using Replate.Application;
using Replate.Infrastructure;
using Replate.Infrastructure.Persistence;
using Replate.Infrastructure.Persistence.Seeds;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Metadata;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //Reject unknown fields 
        options.JsonSerializerOptions.UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;

        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Add Application Layer services (MediatR, AutoMapper, Validators)
builder.Services.AddApplication();

// Add Infrastructure Layer services (DbContext, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new() 
    { 
        Title = "Replate API", 
        Version = "v1", 
        Description = "API for Replate food deals platform" 
    });
    
    // Resolve conflicting schema IDs by using full type name
    s.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

var app = builder.Build();

// Apply migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var context = services.GetRequiredService<ReplateDbContext>();
        
        // Apply any pending migrations
        logger.LogInformation("Applying database migrations...");
        await context.Database.MigrateAsync();
        logger.LogInformation("Database migrations applied successfully.");
        
        // Seed the database
        logger.LogInformation("Starting database seeding...");
        await ApplicationDbContextSeed.SeedAsync(context);
        logger.LogInformation("Database seeding completed.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the DB.");
        throw;
    }
}

// Global Exception Handling Middleware
app.UseMiddleware<Replate.Api.Middleware.GlobalExceptionHandllingMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Replate API V1");
        s.RoutePrefix = "swagger"; // Swagger at: /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Export registered endpoints to api.http for easy testing (written to project folder)
try
{
    var endpointSource = app.Services.GetService<EndpointDataSource>();
    if (endpointSource != null)
    {
        var sb = new StringBuilder();
        foreach (var ep in endpointSource.Endpoints)
        {
            if (ep is RouteEndpoint routeEndpoint)
            {
                var pattern = routeEndpoint.RoutePattern?.RawText ?? routeEndpoint.RoutePattern?.ToString() ?? "/";
                if (!pattern.StartsWith("/")) pattern = "/" + pattern;

                var httpMeta = ep.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault();
                var methods = httpMeta?.HttpMethods ?? new[] { "GET" };

                foreach (var method in methods)
                {
                    sb.AppendLine($"### {method} {pattern}");
                    sb.AppendLine($"{method} http://localhost:5041{pattern}");
                    sb.AppendLine("Accept: application/json");
                    sb.AppendLine();
                }
            }
        }

        var outPath = Path.Combine(Directory.GetCurrentDirectory(), "api.http");
        File.WriteAllText(outPath, sb.ToString());
        Console.WriteLine($"Wrote endpoints to {outPath}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to write api.http: {ex.Message}");
}

app.Run();
