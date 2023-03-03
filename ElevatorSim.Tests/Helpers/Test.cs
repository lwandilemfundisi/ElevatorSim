using AutoFixture;
using AutoFixture.AutoMoq;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Persistence;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ElevatorSim.Tests.Helpers
{
    public abstract class Test
    {
        protected IFixture _fixture { get; private set; }
        protected IServiceProvider _serviceProvider;
        protected IAggregateStore _aggregateStore;
        protected ICommandBus _commandBus;
        protected IQueryProcessor _queryProcessor;

        [SetUp]
        public void Setup() 
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Customize<ElevatorControlId>(x => x.FromFactory(() => ElevatorControlId.New));
            _fixture.Customize<ElevatorId>(x => x.FromFactory(() => ElevatorId.New));
            _fixture.Customize<ManagedElevatorId>(x => x.FromFactory(() => ManagedElevatorId.New));

            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .ConfigureElevatorSimDomain()
                .ConfigureElevatorSimPersistence<ElevatorSimContext, TestElevatorSimContextProvider>()
                .ServiceCollection
                .BuildServiceProvider();

            _aggregateStore = _serviceProvider.GetRequiredService<IAggregateStore>();
            _queryProcessor = _serviceProvider.GetService<IQueryProcessor>();
            _commandBus = _serviceProvider.GetService<ICommandBus>();
        }

        [TearDown]
        public void TearDown()
        {
            ((IDisposable)_serviceProvider).Dispose();
        }

        protected T A<T>()
        {
            return _fixture.Create<T>();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            _fixture.Inject(mock.Object);
            return mock;
        }

        protected Task InitializeElevatorAggregateAsync(ElevatorId id, uint floor, uint weightLimit)
        {
            return UpdateAsync<Elevator, ElevatorId>(id, a => a.InitializeElevator(floor, weightLimit));
        }

        protected Task InitializeElevatorControlAggregateAsync(ElevatorControlId id, InitializeControl initializeControl)
        {
            return UpdateAsync<ElevatorControl, ElevatorControlId>(id, a => a.InitializeElevatorControl(initializeControl));
        }

        protected async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
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
