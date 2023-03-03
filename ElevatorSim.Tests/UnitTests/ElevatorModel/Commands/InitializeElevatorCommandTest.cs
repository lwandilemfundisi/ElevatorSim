using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Persistence;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class InitializeElevatorCommandTest
    {
        private IServiceProvider _serviceProvider;
        private IAggregateStore _aggregateStore;
        private ICommandBus _commandBus;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .ConfigureElevatorSimDomain()
                .ConfigureElevatorSimPersistence<ElevatorSimContext, TestElevatorSimContextProvider>()
                .ServiceCollection
                .BuildServiceProvider();

            _aggregateStore = _serviceProvider.GetRequiredService<IAggregateStore>();
            _commandBus = _serviceProvider.GetService<ICommandBus>();
        }

        [TearDown]
        public void TearDown()
        {
            ((IDisposable)_serviceProvider).Dispose();
        }

        [Test]
        public async Task TestInitializeElevatorCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;

            //Act
            var result = await _commandBus
                .PublishAsync(new InitializeElevatorCommand(testId, 1, 10), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
