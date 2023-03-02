using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence.ElevatorModelPersistence
{
    public class ElevatorContextProvider : IDbContextProvider<ElevatorContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorContext> _options;

        public ElevatorContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ElevatorContext>()
                .UseSqlServer(configuration["DataConnection:ElevatorDatabase"])
                .Options;
        }

        public ElevatorContext CreateContext()
        {
            return new ElevatorContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
