using ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications;
using Microservice.Framework.Common;
using Microservice.Framework.Domain;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects
{
    public class Load
        : ValueObject
    {
        #region Constructors

        public Load(
            uint numberOfPeople,
            uint toFloor) 
        {
            ToFloor = toFloor;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public uint NumberOfPeople { get; }

        public uint ToFloor { get; }

        #endregion

        #region Specifications

        public ISpecification<Elevator> GetWeightSpecification()
        {
            return new WeightLimitSpecification(NumberOfPeople);
        }

        #endregion
    }
}
