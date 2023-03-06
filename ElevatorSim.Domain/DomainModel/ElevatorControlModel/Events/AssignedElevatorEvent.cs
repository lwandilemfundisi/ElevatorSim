using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events
{
    [EventVersion("AssignedElevatorEvent", 1)]
    public class AssignedElevatorEvent
        : AggregateEvent<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public AssignedElevatorEvent(AssignedElevator assignedElevetor)
        {
            AssignedElevetor = assignedElevetor;
        }

        #endregion

        #region Properties

        public AssignedElevator AssignedElevetor { get; }

        #endregion
    }
}
