using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Persistence;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class LoadPeopleCommandTest
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
        public async Task TestLoadPeopleCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var load = new Load(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new LoadPeopleCommand(testId, load), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task TestLoadPeopleCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var load = new Load(11);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new LoadPeopleCommand(testId, load), CancellationToken.None);

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
