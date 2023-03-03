using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence
{
    public class ElevatorSimContextFactory : IDesignTimeDbContextFactory<ElevatorSimContext>
    {
        public ElevatorSimContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ElevatorSimContext>();
            optionsBuilder.UseSqlServer(configuration["DataConnection:ElevatorSimDatabase"]);

            return new ElevatorSimContext(optionsBuilder.Options);
        }
    }
}
