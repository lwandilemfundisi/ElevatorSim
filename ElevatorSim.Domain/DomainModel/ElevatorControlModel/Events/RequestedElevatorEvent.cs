using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events
{
    [EventVersion("RequestedElevatorEvent", 1)]
    public class RequestedElevatorEvent
        : AggregateEvent<ElevatorControl, ElevatorControlId>
    {
        public RequestedElevatorEvent(
            RequestElevetor requestElevetor,
            string selectedElevatorId)
        {
            RequestElevetor = requestElevetor;
            SelectedElevatorId = selectedElevatorId;
        }

        public string SelectedElevatorId { get; }
        public RequestElevetor RequestElevetor { get; }
    }
}
