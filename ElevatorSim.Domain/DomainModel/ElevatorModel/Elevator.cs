using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications;
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

        #region Properties

        public uint CurrentFloor { get; set; }

        public uint Weightlimit { get; set; }

        #endregion

        #region Methods

        public void InitializeElevator()
        {
            AggregateSpecifications
                .AggregateIsNew
                .ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ElevatorInitializedEvent());
        }

        public void DisableElevator()
        {
            AggregateSpecifications
                .AggregateIsNew
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new ElevatorDisabledEvent());
        }

        public void MoveUp(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetWeightSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new ElevatorMovedUpEvent());
        }

        public void MoveDown(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetWeightSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new ElevatorMovedDownEvent());
        }

        #endregion
    }
}
