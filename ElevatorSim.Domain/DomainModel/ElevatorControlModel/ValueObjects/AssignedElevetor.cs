using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class AssignedElevetor
        : ValueObject
    {
        #region Constructors

        public AssignedElevetor(
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
