using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class ElevatorMovedUpEventSubscriber
        : ISubscribeSynchronousTo<Elevator, ElevatorId, ElevatorMovedUpEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<ElevatorMovedUpEventSubscriber> _logger;

        #region Constructors

        public ElevatorMovedUpEventSubscriber(
            ICommandBus commandBus,
            ILogger<ElevatorMovedUpEventSubscriber> logger)
        {
            _logger = logger;
            _commandBus = commandBus;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<Elevator, ElevatorId, ElevatorMovedUpEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Elevator has now moved up the load!");
            await _commandBus.PublishAsync(new DeliverLoadCommand(
                domainEvent.AggregateIdentity,
                new DeliverLoad(domainEvent.AggregateEvent.WithWeight)), cancellationToken);
        }

        #endregion
    }
}
