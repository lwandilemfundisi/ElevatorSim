using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Subscribers;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class ElevatorMovedUpEventSubscriber
        : ISubscribeSynchronousTo<Elevator, ElevatorId, ElevatorMovedUpEvent>
    {
        private readonly ICommandBus _commandBus;

        #region Constructors

        public ElevatorMovedUpEventSubscriber(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<Elevator, ElevatorId, ElevatorMovedUpEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new DeliverLoadCommand(
                domainEvent.AggregateIdentity,
                new DeliverLoad(domainEvent.AggregateEvent.WithWeight)), cancellationToken);
        }

        #endregion
    }
}
