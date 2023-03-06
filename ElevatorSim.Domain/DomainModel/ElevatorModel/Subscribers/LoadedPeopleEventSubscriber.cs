using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Subscribers
{
    public class LoadedPeopleEventSubscriber
        : ISubscribeSynchronousTo<Elevator, ElevatorId, LoadedPeopleEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger<LoadedPeopleEventSubscriber> _logger;

        #region Constructors

        public LoadedPeopleEventSubscriber(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            ILogger<LoadedPeopleEventSubscriber> logger) 
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<Elevator, ElevatorId, LoadedPeopleEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Elevator has now taken the load!");
            var elevator = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(domainEvent.AggregateIdentity), cancellationToken);

            //check where elevator is, then decide on that if its up or down
            if (elevator.CurrentFloor > domainEvent.AggregateEvent.ToFloor)
                await _commandBus
                    .PublishAsync(new RequestElevatorDownCommand(
                        domainEvent.AggregateIdentity,
                        new Move(
                            domainEvent.AggregateEvent.ToFloor,
                            domainEvent.AggregateEvent.NumberOfPeople)), cancellationToken);

            else if (elevator.CurrentFloor < domainEvent.AggregateEvent.ToFloor)
                await _commandBus
                    .PublishAsync(new RequestElevatorUpCommand(
                        domainEvent.AggregateIdentity,
                        new Move(
                            domainEvent.AggregateEvent.ToFloor,
                            domainEvent.AggregateEvent.NumberOfPeople)), cancellationToken);
        }

        #endregion
    }
}
