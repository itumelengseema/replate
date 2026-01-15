var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(
    s =>
    {
        s.SwaggerDoc("v1", new() { Title = "Replate API", Version = "v1", Description = "API for Replate food deals plateform" });
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(
        s =>
        {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "Replate API V1");
            s.RoutePrefix = string.Empty; // Swagger is accessible at root: https://localhost:5001/
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
