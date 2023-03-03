using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElevatorSim.Persistence
{
    public class ElevatorSimContextProvider : IDbContextProvider<ElevatorSimContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorSimContext> _options;

        public ElevatorSimContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ElevatorSimContext>()
                .UseSqlServer(configuration["DataConnection:ElevatorSimDatabase"])
                .Options;
        }

        public ElevatorSimContext CreateContext()
        {
            return new ElevatorSimContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
