using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Extensions;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel
{
    public class Elevator 
        : AggregateRoot<Elevator, ElevatorId>
    {
        private Action<object, string> LazyLoader { get; set; }

        #region Constructors

        public Elevator()
            : base(null)
        {
        }

        public Elevator(ElevatorId id)
            : base(id)
        {
        }


        #endregion

        #region Methods

        public void MoveUp()
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ElevatorMovedUpEvent());
        }

        public void MoveDown()
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ElevatorMovedDownEvent());
        }

        #endregion
    }
}
