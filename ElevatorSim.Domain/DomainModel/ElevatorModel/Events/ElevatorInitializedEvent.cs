using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("ElevatorInitializedEvent", 1)]
    public class ElevatorInitializedEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
    }
}
