using ElevatorSim.Persistence.ElevatorControlModelPersistence;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Tests.Helpers
{
    public class TestElevatorControlContextProvider : IDbContextProvider<ElevatorControlContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorControlContext> _options;

        public TestElevatorControlContextProvider()
        {
            _options = new DbContextOptionsBuilder<ElevatorControlContext>()
                .UseInMemoryDatabase(databaseName: "ElevatorControlDb")
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
