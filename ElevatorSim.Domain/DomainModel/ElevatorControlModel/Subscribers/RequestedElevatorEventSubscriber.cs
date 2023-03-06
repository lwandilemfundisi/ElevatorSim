using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class RequestedElevatorEventSubscriber
        : ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, RequestedElevatorEvent>
    {
        private readonly ILogger<RequestedElevatorEventSubscriber> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        #region Constructors

        public RequestedElevatorEventSubscriber(
            ILogger<RequestedElevatorEventSubscriber> logger,
            IQueryProcessor queryProcessor,
            ICommandBus commandBus)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, RequestedElevatorEvent> domainEvent,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting an elevator from pool!");

            var elevatorControl = await _queryProcessor.ProcessAsync(
                new GetElevatorControlQuery(domainEvent.AggregateIdentity), cancellationToken);

            if (!elevatorControl.Elevators.HasItems())
                throw new Exception("No elevators to select from!");

            var elevatorCount = elevatorControl.Elevators.Count();
            var roundRobinIdx = (int)domainEvent.AggregateEvent.RequestElevetor.ToFloorNumber % elevatorCount;
            var selectedElevatorId = elevatorControl.Elevators[roundRobinIdx].ElevatorId;

            await _commandBus.PublishAsync(new AssignElevatorCommand(
                domainEvent.AggregateIdentity,
                new AssignedElevator(
                    selectedElevatorId,
                    domainEvent.AggregateEvent.RequestElevetor.FromFloor,
                    domainEvent.AggregateEvent.RequestElevetor.ToFloorNumber,
                    domainEvent.AggregateEvent.RequestElevetor.NumberOfPeople)
                ), cancellationToken);
        }

        #endregion
    }
}
