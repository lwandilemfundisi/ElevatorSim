using ElevatorSim.Domain.DomainModel.ElevatorModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Extensions;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel
{
    public class Elevator 
        : AggregateRoot<Elevator, ElevatorId>
    {
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

        public ElevatorStatus ElevatorStatus { get; set; }

        #endregion

        #region Methods

        public void InitializeElevator(
            uint floor,
            uint weightLimit
            )
        {
            AggregateSpecifications
                .AggregateIsNew
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentFloor = floor;
            Weightlimit = weightLimit;
            ElevatorStatus = ElevatorStatuses.Of().InOperation;

            Emit(new ElevatorInitializedEvent());
        }

        public void DisableElevator()
        {
            AggregateSpecifications
                .AggregateIsNew
                .ThrowDomainErrorIfNotSatisfied(this);

            ElevatorStatus = ElevatorStatuses.Of().Disabled;

            Emit(new ElevatorDisabledEvent());
        }

        public void MoveUp(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetWeightSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentFloor = move.FloorMovingTo;
            Weightlimit = move.Weight;

            Emit(new ElevatorMovedUpEvent());
        }

        public void MoveDown(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetWeightSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentFloor = move.FloorMovingTo;
            Weightlimit = move.Weight;

            Emit(new ElevatorMovedDownEvent());
        }

        #endregion
    }
}
