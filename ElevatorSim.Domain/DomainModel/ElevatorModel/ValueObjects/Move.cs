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
            uint floorMovingTo,
            uint weight)
        {
            Weight = weight;
            FloorMovingTo = floorMovingTo;
        }

        #endregion

        #region Properties

        public uint FloorMovingTo { get; }

        public uint Weight { get; }

        #endregion

        #region Methods

        public Specification<Elevator> GetWeightSpecification()
        {
            return new WeightLimitSpecification(Weight);
        }

        #endregion
    }
}
