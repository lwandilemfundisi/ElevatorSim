using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events
{
    [EventVersion("RequestedElevatorEvent", 1)]
    public class RequestedElevatorEvent
        : AggregateEvent<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public RequestedElevatorEvent(RequestElevetor requestElevetor)
        {
            RequestElevetor = requestElevetor;
        }

        #endregion

        #region Properties

        public RequestElevetor RequestElevetor { get; }

        #endregion
    }
}
