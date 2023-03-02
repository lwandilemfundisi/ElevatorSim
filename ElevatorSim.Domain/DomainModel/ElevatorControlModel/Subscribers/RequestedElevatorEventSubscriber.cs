using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Subscribers;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class RequestedElevatorEventSubscriber
        : ISubscribeAsynchronousTo<ElevatorControl, ElevatorControlId, RequestedElevatorEvent>
    {
        #region Constructors

        public RequestedElevatorEventSubscriber()
        {
        }

        #endregion

        #region Virtual Methods

        public Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, RequestedElevatorEvent> domainEvent,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
