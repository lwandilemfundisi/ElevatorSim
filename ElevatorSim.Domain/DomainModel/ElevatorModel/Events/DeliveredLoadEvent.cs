using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("DeliveredLoadEvent", 1)]
    public class DeliveredLoadEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public DeliveredLoadEvent(
            uint deliveredLoad,
            uint floorDeliveredTo) 
        {
            DeliveredLoad = deliveredLoad;
            FloorDeliveredTo = floorDeliveredTo;
        }

        #endregion

        #region Properties

        public uint FloorDeliveredTo { get; }

        public uint DeliveredLoad { get; }

        #endregion
    }
}
