using Replate.Application;
using Replate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

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
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Replate API V1");
        s.RoutePrefix = string.Empty; // Swagger at root: https://localhost:5001/
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();