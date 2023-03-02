using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence.ElevatorControlModelPersistence
{
    public class ElevatorControlContextProvider : IDbContextProvider<ElevatorControlContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorControlContext> _options;

        public ElevatorControlContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ElevatorControlContext>()
                .UseSqlServer(configuration["DataConnection:ElevatorControlDatabase"])
                .Options;
        }

        public ElevatorControlContext CreateContext()
        {
            return new ElevatorControlContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
