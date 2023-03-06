using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class ElevatorMovedDownEventSubscriber
        : ISubscribeSynchronousTo<Elevator, ElevatorId, ElevatorMovedDownEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger<ElevatorMovedDownEventSubscriber> _logger;

        #region Constructors

        public ElevatorMovedDownEventSubscriber(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            ILogger<ElevatorMovedDownEventSubscriber> logger)
        {
            _logger = logger;
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<Elevator, ElevatorId, ElevatorMovedDownEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Elevator has moved down to {domainEvent.AggregateEvent.MovedToFloor}!");

            //get elevator status
            var elevator = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(
                    domainEvent.AggregateIdentity), cancellationToken);

            if (!domainEvent.AggregateEvent.IsMovingLoad)
            {
                await _commandBus.PublishAsync(new LoadPeopleCommand(
                    domainEvent.AggregateIdentity, new Load(
                        domainEvent.AggregateEvent.WithWeight,
                        domainEvent.AggregateEvent.TakingLoadToFloor.GetValueOrDefault())), cancellationToken);
            }
            else if(domainEvent.AggregateEvent.IsMovingLoad)
            {
                _logger.LogInformation("Elevator has now moved down the load!");
                await _commandBus.PublishAsync(new DeliverLoadCommand(
                    domainEvent.AggregateIdentity,
                    new DeliverLoad(domainEvent.AggregateEvent.WithWeight)), cancellationToken);
            }
            else
            {
                throw new Exception("Something went wrong!");
            }
        }

        #endregion
    }
}
