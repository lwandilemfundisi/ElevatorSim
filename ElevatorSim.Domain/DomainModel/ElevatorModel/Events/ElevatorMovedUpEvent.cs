using Microservice.Framework.Domain.Events.AggregateEvents;
using Microservice.Framework.Domain.Events;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("ElevatorMovedUpEvent", 1)]
    public class ElevatorMovedUpEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public ElevatorMovedUpEvent()
        {

        }

        #endregion

        #region Properties

        #endregion
    }
}
