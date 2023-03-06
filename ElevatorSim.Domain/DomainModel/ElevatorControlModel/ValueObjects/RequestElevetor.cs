using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class RequestElevetor
        : ValueObject
    {
        #region Constructors

        public RequestElevetor(
            uint fromFloor,
            uint tofloorNumber,
            uint numberOfPeople)
        {
            FromFloor = fromFloor;
            ToFloorNumber = tofloorNumber;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public uint FromFloor { get; }

        public uint ToFloorNumber { get; }

        public uint NumberOfPeople { get; }

        #endregion
    }
}
