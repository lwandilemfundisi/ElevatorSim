using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events
{
    [EventVersion("MoveElevatorEvent", 1)]
    public class MoveElevatorEvent
        : AggregateEvent<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public MoveElevatorEvent(string elevatorId, uint fromFloor, uint toFloor, uint toLoadPeople)
        {
            ElevatorId = elevatorId;
            FromFloor = fromFloor;
            ToFloor = toFloor;
            ToLoadPeople = toLoadPeople;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }
        public uint FromFloor { get; }
        public uint ToFloor { get; }
        public uint ToLoadPeople { get; }

        #endregion
    }
}
