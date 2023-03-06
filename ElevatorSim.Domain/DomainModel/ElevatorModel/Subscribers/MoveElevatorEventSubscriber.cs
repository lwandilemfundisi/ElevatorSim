using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class MoveElevatorEventSubscriber
        : ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, MoveElevatorEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger<MoveElevatorEventSubscriber> _logger;

        #region Constructors

        public MoveElevatorEventSubscriber(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            ILogger<MoveElevatorEventSubscriber> logger)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, MoveElevatorEvent> domainEvent,
            CancellationToken cancellationToken)
        {
            var elevatorId = new ElevatorId(domainEvent.AggregateEvent.ElevatorId);
            var requestedFromFloor = domainEvent.AggregateEvent.FromFloor;
            var goingToFloor = domainEvent.AggregateEvent.ToFloor;
            var elevator = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(elevatorId), cancellationToken);

            //check where elevator is, then decide on that if its up or down
            if (elevator.CurrentFloor > requestedFromFloor)
            {
                _logger.LogInformation($"Elevator has been requested to come down to {requestedFromFloor}!");
                await _commandBus
                    .PublishAsync(new RequestElevatorDownCommand(
                        elevatorId,
                        new Move(
                            requestedFromFloor,
                            goingToFloor,
                            domainEvent.AggregateEvent.ToLoadPeople,
                            false)), cancellationToken);
            }
            else if (elevator.CurrentFloor < requestedFromFloor)
            {
                _logger.LogInformation($"Elevator has been requested to come up to {requestedFromFloor}!");
                await _commandBus
                    .PublishAsync(new RequestElevatorUpCommand(
                        elevatorId,
                        new Move(
                            requestedFromFloor,
                            goingToFloor,
                            domainEvent.AggregateEvent.ToLoadPeople,
                            false)), cancellationToken);
            }
            else
            {
                _logger.LogInformation($"Elevator has arrived at your floor {requestedFromFloor}!");
                //if the elevetor is at the requested floor, then it loads
                await _commandBus.PublishAsync(new LoadPeopleCommand(
                    elevatorId, new Load(
                        domainEvent.AggregateEvent.ToLoadPeople,
                        domainEvent.AggregateEvent.ToFloor)), cancellationToken);
            }
                
        }

        #endregion
    }
}
