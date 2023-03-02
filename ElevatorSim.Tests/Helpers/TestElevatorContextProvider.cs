using ElevatorSim.Persistence.ElevatorModelPersistence;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Tests.Helpers
{
    public class TestElevatorContextProvider : IDbContextProvider<ElevatorContext>, IDisposable
    {
        private readonly DbContextOptions<ElevatorContext> _options;

        public TestElevatorContextProvider()
        {
            _options = new DbContextOptionsBuilder<ElevatorContext>()
                .UseInMemoryDatabase(databaseName: "ElevatorDb")
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
