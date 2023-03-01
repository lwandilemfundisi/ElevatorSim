using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class RequestElevetor
        : ValueObject
    {
        #region Constructors

        public RequestElevetor(
            uint floorNumber)
        {
            FloorNumber = floorNumber;
        }

        #endregion

        #region Properties

        public uint FloorNumber { get; }

        #endregion
    }
}
