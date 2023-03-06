using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class AssignedElevator
        : ValueObject
    {
        #region Constructors

        public AssignedElevator(
            string elevatorId,
            uint fromfloorNumber,
            uint tofloorNumber,
            uint numberOfPeople)
        {
            ElevatorId = elevatorId;
            FromFloorNumber = fromfloorNumber;
            ToFloorNumber = tofloorNumber;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }

        public uint FromFloorNumber { get; }

        public uint ToFloorNumber { get; }

        public uint NumberOfPeople { get; }

        #endregion
    }
}
