using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Events.AggregateEvents;
using Microservice.Framework.Domain.Events;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events
{
    [EventVersion("InitializedElevatorControlEvent", 1)]
    public class InitializedElevatorControlEvent
        : AggregateEvent<ElevatorControl, ElevatorControlId>
    {
    }
}
