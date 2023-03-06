using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("ElevatorMovedDownEvent", 1)]
    public class ElevatorMovedDownEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public ElevatorMovedDownEvent(
            uint movedToFloor,
            uint withWeight)
        {
            WithWeight = withWeight;
            MovedToFloor = movedToFloor;
        }

        #endregion

        #region Properties

        public uint MovedToFloor { get; }
        public uint WithWeight { get; }

        #endregion
    }
}
