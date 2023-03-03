using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Persistence.ElevatorModelPersistence;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Queries
{
    [Category("Unit")]
    public class GetElevatorQueryTest
    {
        private IServiceProvider _serviceProvider;
        private IAggregateStore _aggregateStore;
        private IQueryProcessor _queryProcessor;

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
            _queryProcessor = _serviceProvider.GetService<IQueryProcessor>();
        }

        [TearDown]
        public void TearDown()
        {
            ((IDisposable)_serviceProvider).Dispose();
        }

        [Test]
        public async Task TestGetElevatorQuery_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(testId), CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(testId);
            result.CurrentFloor.Should().Be(1);
            result.Weightlimit.Should().Be(10);
        }

        [Test]
        public async Task TestGetElevatorQuery_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            AsyncTestDelegate act = () => _queryProcessor
                .ProcessAsync(new GetElevatorQuery(null), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<InvariantException>());
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
