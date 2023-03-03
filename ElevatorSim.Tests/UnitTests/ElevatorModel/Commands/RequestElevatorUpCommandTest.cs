using ElevatorSim.Persistence.ElevatorModelPersistence;
using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using Microservice.Framework.Common;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using FluentAssertions;
using Microservice.Framework.Domain.Exceptions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    public class RequestElevatorUpCommandTest
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
                .ConfigureElevatorSimPersistence<ElevatorContext, TestElevatorContextProvider>()
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
        public async Task TestRequestElevatorUpCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new RequestElevatorUpCommand(testId, move), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task TestRequestElevatorUpCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(1);
            await InitializeElevatorAggregateAsync(testId, 2, 10);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new RequestElevatorUpCommand(testId, move), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }

        #region Private Helper Methods

        private Task InitializeElevatorAggregateAsync(ElevatorId id, uint floor, uint weightLimit)
        {
            return UpdateAsync<Elevator, ElevatorId>(id, a => a.InitializeElevator(floor, weightLimit));
        }

        private async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : class, IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
                id,
                SourceId.New,
                (a, c) =>
                {
                    action(a);
                    return Task.FromResult(0);
                },
                CancellationToken.None);
        }

        #endregion
    }
}
