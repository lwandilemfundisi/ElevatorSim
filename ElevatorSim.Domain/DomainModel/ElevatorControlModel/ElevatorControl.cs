using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Domain.Extensions;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Extensions;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel
{
    public class ElevatorControl 
        : AggregateRoot<ElevatorControl, ElevatorControlId>
    {
        private IList<ManagedElevator> _elevators;
        private Action<object, string> LazyLoader { get; set; }

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

        #region Properties

        public IList<ManagedElevator> Elevators 
        {
            get => LazyLoader.Load(this, ref _elevators);
            set => _elevators = value;
        }

        #endregion

        #region Methods

        public void InitializeElevatorControl(InitializeControl initializeControl)
        {
            AggregateSpecifications
                .AggregateIsNew
                .And(initializeControl.GetAtLeastOneElevatorSpecification())
                .ThrowDomainErrorIfNotSatisfied(this);

            Elevators = initializeControl.Elevators;

            Emit(new InitializedElevatorControlEvent());
        }

        public void RequestElevator(RequestElevetor requestElevetor)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

            Emit(new RequestedElevatorEvent(requestElevetor));
        }

        public void AssignElevator(AssignedElevetor assignedElevetor)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

            Emit(new AssignedElevatorEvent(assignedElevetor));
        }

        public void MoveElevator(string elevatorId, uint toFloor, uint toLoadPeople)
        {
            AggregateSpecifications
                .AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

            Emit(new MoveElevatorEvent(elevatorId, toFloor, toLoadPeople));
        }

        #endregion
    }
}
