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

        public uint CurrentWeight { get; set; }

        public ElevatorStatus ElevatorStatus { get; set; }

        #endregion

        #region Methods

        public void InitializeElevator(
            uint floor,
            uint weightLimit,
            uint currentWeight = 0
            )
        {
            AggregateSpecifications
                .AggregateIsNew
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentWeight = currentWeight;
            CurrentFloor = floor;
            Weightlimit = weightLimit;
            ElevatorStatus = ElevatorStatuses.Of().InReady;

            Emit(new ElevatorInitializedEvent());
        }

        public void DisableElevator()
        {
            AggregateSpecifications
                .AggregateIsCreated
                .ThrowDomainErrorIfNotSatisfied(this);

            ElevatorStatus = ElevatorStatuses.Of().Disabled;

            Emit(new ElevatorDisabledEvent());
        }

        public void MoveUp(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetElevatorMoveUpSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentFloor = move.RequestedFromFloor ?? move.FloorMovingTo;
            ElevatorStatus = ElevatorStatuses.Of().InOperation;

            Emit(new ElevatorMovedUpEvent(CurrentFloor, move.Weight, move.IsMovingLoad, move.FloorMovingTo));
        }

        public void MoveDown(Move move)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(move.GetElevatorMoveDownSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentFloor = move.RequestedFromFloor ?? move.FloorMovingTo;
            ElevatorStatus = ElevatorStatuses.Of().InOperation;

            Emit(new ElevatorMovedDownEvent(CurrentFloor, move.Weight, move.IsMovingLoad, move.FloorMovingTo));
        }

        public void LoadPeople(Load load)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(load.GetWeightSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentWeight = load.NumberOfPeople;
            ElevatorStatus = ElevatorStatuses.Of().InLoading;

            Emit(new LoadedPeopleEvent(load.NumberOfPeople, load.ToFloor));
        }

        public void DeliverLoad(DeliverLoad loadToDeliver)
        {
            AggregateSpecifications
                .AggregateIsCreated
                .And(loadToDeliver.GetDeliverLoadSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            CurrentWeight -= loadToDeliver.LoadToDeliver;
            ElevatorStatus = ElevatorStatuses.Of().InReady;

            Emit(new DeliveredLoadEvent(loadToDeliver.LoadToDeliver, CurrentFloor));
        }

        #endregion
    }
}
