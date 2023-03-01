using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects
{
    public class Move
        : ValueObject
    {
        #region Constructors

        public Move(uint floorMovingTo)
        {
            FloorMovingTo = floorMovingTo;
        }

        #endregion

        #region Properties

        public uint FloorMovingTo { get; }

        #endregion
    }
}
