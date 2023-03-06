using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("DeliveredLoadEvent", 1)]
    public class DeliveredLoadEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public DeliveredLoadEvent(uint deliveredLoad) 
        {
            DeliveredLoad = deliveredLoad;
        }

        #endregion

        #region Properties

        public uint DeliveredLoad { get; }

        #endregion
    }
}
