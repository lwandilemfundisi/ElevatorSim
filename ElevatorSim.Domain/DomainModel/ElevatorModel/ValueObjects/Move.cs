using ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications;
using Microservice.Framework.Common;
using Microservice.Framework.Domain;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects
{
    public class Move
        : ValueObject
    {
        #region Constructors

        public Move(
            uint? requestedFromFloor,
            uint floorMovingTo, 
            uint weight,
            bool isMovingLoad)
        {
            Weight = weight;
            RequestedFromFloor = requestedFromFloor;
            IsMovingLoad = isMovingLoad;
            FloorMovingTo = floorMovingTo;
        }

        #endregion

        #region Properties

        public uint? RequestedFromFloor { get; }

        public bool IsMovingLoad { get; }

        public uint Weight { get; }

        public uint FloorMovingTo { get; }

        #endregion

        #region Specifications

        public ISpecification<Elevator> GetElevatorMoveDownSpecification()
        {
            return new ElevatorMoveDownSpecification(FloorMovingTo);
        }

        public ISpecification<Elevator> GetElevatorMoveUpSpecification()
        {
            return new ElevatorMoveUpSpecification(FloorMovingTo);
        }

        #endregion
    }
}
