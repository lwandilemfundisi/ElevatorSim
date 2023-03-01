using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
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

        public void MoveUp(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ElevatorMovedUpEvent());
        }

        public void MoveDown(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ElevatorMovedDownEvent());
        }

        #endregion
    }
}
