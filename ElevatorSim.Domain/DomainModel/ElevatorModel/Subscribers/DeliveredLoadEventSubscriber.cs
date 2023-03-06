using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class DeliveredLoadEventSubscriber
        : ISubscribeSynchronousTo<Elevator, ElevatorId, DeliveredLoadEvent>
    {
        private readonly ILogger<DeliveredLoadEventSubscriber> _logger;

        #region Constructors

        public DeliveredLoadEventSubscriber(
            ILogger<DeliveredLoadEventSubscriber> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Virtual Methods

        public Task HandleAsync(
            IDomainEvent<Elevator, ElevatorId, DeliveredLoadEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Elevator has now delivered load of {domainEvent.AggregateEvent.DeliveredLoad} " +
                $"to floor {domainEvent.AggregateEvent.FloorDeliveredTo}!");
            return Task.CompletedTask;
        }

        #endregion
    }
}
