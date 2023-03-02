using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class RequestElevetor
        : ValueObject
    {
        #region Constructors

        public RequestElevetor(
            uint floorNumber,
            uint numberOfPeople)
        {
            FloorNumber = floorNumber;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public uint FloorNumber { get; }

        public uint NumberOfPeople { get; }

        #endregion
    }
}
