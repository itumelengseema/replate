using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Replate.Infrastructure.Persistence;

public class ReplateDbContextFactory: IDesignTimeDbContextFactory<ReplateDbContext>
{
    public ReplateDbContext CreateDbContext(string[] args)
    {
        //Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Replate.Api"))
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

       // Get connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<ReplateDbContext>();
        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Replate.Infrastructure"));

        return new ReplateDbContext(optionsBuilder.Options);
    }
}