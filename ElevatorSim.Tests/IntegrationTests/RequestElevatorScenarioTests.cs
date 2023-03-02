using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Persistence.ElevatorModelPersistence;
using ElevatorSim.Persistence.Extensions;
using Microservice.Framework.Common;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using System.Drawing;
using Microservice.Framework.Domain.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using FluentAssertions;

namespace ElevatorSim.Tests.IntegrationTests
{
    public class RequestElevatorScenarioTests : Test
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
            _commandBus = _serviceProvider.GetRequiredService<ICommandBus>();
        }

        [TearDown]
        public void TearDown()
        {
            ((IDisposable)_serviceProvider).Dispose();
        }

        [Test]
        public async Task TestJust()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);
            var queryProcessor = _serviceProvider.GetService<IQueryProcessor>();

            //Act
            var result = await queryProcessor.ProcessAsync(new GetElevatorQuery(testId), CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(testId);
        }

        public Task InitializeElevatorAggregateAsync(ElevatorId id, uint floor, uint weightLimit)
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
    }
}
