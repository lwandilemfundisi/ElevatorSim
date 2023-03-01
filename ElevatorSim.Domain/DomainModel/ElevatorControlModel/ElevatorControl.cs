using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Extensions;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel
{
    public class ElevatorControl 
        : AggregateRoot<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public ElevatorControl()
            : base(null)
        {
        }

        public ElevatorControl(ElevatorControlId id)
            : base(id)
        {
        }

        #endregion

        #region Methods

        public void RequestElevator(RequestElevetor requestElevetor)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new RequestedElevatorEvent(requestElevetor));
        }

        #endregion
    }
}
