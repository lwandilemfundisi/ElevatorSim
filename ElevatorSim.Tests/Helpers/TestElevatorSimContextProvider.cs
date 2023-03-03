using ElevatorSim.Persistence;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Tests.Helpers
{
    public class TestElevatorSimContextProvider : IDbContextProvider<ElevatorSimContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorSimContext> _options;

        public TestElevatorSimContextProvider()
        {
            _options = new DbContextOptionsBuilder<ElevatorSimContext>()
                .UseInMemoryDatabase(databaseName: "ElevatorSimDb")
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
