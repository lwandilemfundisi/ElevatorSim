using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class AssignedElevator
        : ValueObject
    {
        #region Constructors

        public AssignedElevator(
            string elevatorId,
            uint floorNumber,
            uint numberOfPeople)
        {
            ElevatorId = elevatorId;
            FloorNumber = floorNumber;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }

        public uint FloorNumber { get; }

        public uint NumberOfPeople { get; }

        #endregion
    }
}
