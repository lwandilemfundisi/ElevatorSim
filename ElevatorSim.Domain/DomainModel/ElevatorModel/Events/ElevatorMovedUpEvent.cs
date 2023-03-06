using Microservice.Framework.Domain.Events.AggregateEvents;
using Microservice.Framework.Domain.Events;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("ElevatorMovedUpEvent", 1)]
    public class ElevatorMovedUpEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public ElevatorMovedUpEvent(
            uint movedToFloor, 
            uint withWeight,
            bool isMovingLoad,
            uint? takingLoadToFloor)
        {
            IsMovingLoad = isMovingLoad;
            TakingLoadToFloor = takingLoadToFloor;
            WithWeight = withWeight;
            MovedToFloor = movedToFloor;
        }

        #endregion

        #region Properties

        public bool IsMovingLoad { get; }
        public uint? TakingLoadToFloor { get; }
        public uint MovedToFloor { get; }
        public uint WithWeight { get; }

        #endregion
    }
}
