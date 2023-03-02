using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Queries;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Subscribers;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class RequestedElevatorEventSubscriber
        : ISubscribeAsynchronousTo<ElevatorControl, ElevatorControlId, RequestedElevatorEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        #region Constructors

        public RequestedElevatorEventSubscriber(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        #endregion

        #region Virtual Methods

        public async Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, RequestedElevatorEvent> domainEvent,
            CancellationToken cancellationToken)
        {
            var elevatorControl = await _queryProcessor.ProcessAsync(
                new GetElevatorControlQuery(domainEvent.AggregateIdentity), cancellationToken);

            if (!elevatorControl.Elevators.HasItems())
                throw new Exception("No elevators to select from!");

            var elevatorCount = elevatorControl.Elevators.Count();
            var roundRobinIdx = (int)domainEvent.AggregateEvent.RequestElevetor.FloorNumber % elevatorCount;
            var selectedElevatorId = elevatorControl.Elevators[roundRobinIdx].ElevatorId;

            await _commandBus.PublishAsync(new AssignElevatorCommand(
                domainEvent.AggregateIdentity,
                selectedElevatorId,
                domainEvent.AggregateEvent.RequestElevetor.FloorNumber,
                domainEvent.AggregateEvent.RequestElevetor.NumberOfPeople
                ), cancellationToken);
        }

        #endregion
    }
}
