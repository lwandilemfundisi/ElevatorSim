using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;
using Microservice.Framework.Domain.Subscribers;
using Moq;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Subscribers
{
    public class MoveElevatorEventSubscriberTests 
        : TestsFor<DispatchToEventSubscribers>
    {
        private Mock<IServiceProvider> _serviceProviderMock;
        private LoggerMock<DispatchToEventSubscribers> _logMock;

        [SetUp]
        public void SetUp()
        {
            _logMock = new LoggerMock<DispatchToEventSubscribers>();
            _serviceProviderMock = InjectMock<IServiceProvider>();
        }

        [Test]
        public async Task MoveElevatorEventSubscriberGetCalled()
        {
            // Arrange
            var subscriberMock = ArrangeSynchronousSubscriber<MoveElevatorEvent>();

            // Act
            await Sut.DispatchToSynchronousSubscribersAsync(
                new[] 
                { 
                    A<DomainEvent<ElevatorControl, ElevatorControlId, MoveElevatorEvent>>() 
                }, 
                CancellationToken.None).ConfigureAwait(false);

            // Assert
            subscriberMock.Verify(s => s.HandleAsync(
                It.IsAny<IDomainEvent<ElevatorControl, ElevatorControlId, MoveElevatorEvent>>(), 
                It.IsAny<CancellationToken>()), Times.Once);

            _logMock.VerifyNoProblems();
        }

        private Mock<ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, TEvent>> ArrangeSynchronousSubscriber<TEvent>()
            where TEvent : IAggregateEvent<ElevatorControl, ElevatorControlId>
        {
            var subscriberMock = 
                new Mock<ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, TEvent>>();

            _serviceProviderMock
                .Setup(r => r.GetService(
                    typeof(IEnumerable<ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, TEvent>>)))
                .Returns(new object[] { subscriberMock.Object });

            return subscriberMock;
        }
    }
}
