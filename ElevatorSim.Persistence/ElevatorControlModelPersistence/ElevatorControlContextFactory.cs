using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence.ElevatorControlModelPersistence
{
    public class ElevatorControlContextFactory : IDesignTimeDbContextFactory<ElevatorControlContext>
    {
        public ElevatorControlContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ElevatorControlContext>();
            optionsBuilder.UseSqlServer(configuration["DataConnection:ElevatorControlDatabase"]);

            return new ElevatorControlContext(optionsBuilder.Options);
        }
    }
}
