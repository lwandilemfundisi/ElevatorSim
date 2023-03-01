using ElevatorSim.Persistence.ElevatorControlModelPersistence;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence.ElevatorModelPersistence
{
    public class ElevatorContextFactory : IDesignTimeDbContextFactory<ElevatorContext>
    {
        public ElevatorContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ElevatorContext>();
            optionsBuilder.UseSqlServer(configuration["DataConnection:Database"]);

            return new ElevatorContext(optionsBuilder.Options);
        }
    }
}
