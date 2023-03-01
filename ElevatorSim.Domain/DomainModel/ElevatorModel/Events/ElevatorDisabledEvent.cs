using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("ElevatorDisabledEvent", 1)]
    public class ElevatorDisabledEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
    }
}
