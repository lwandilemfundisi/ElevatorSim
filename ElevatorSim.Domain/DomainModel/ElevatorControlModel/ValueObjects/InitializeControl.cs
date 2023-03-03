using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Specifications;
using Microservice.Framework.Common;
using Microservice.Framework.Domain;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects
{
    public class InitializeControl
        : ValueObject
    {
        #region Constructors

        public InitializeControl(IList<ManagedElevator> elevators)
        {
            Elevators = elevators;
        }

        #endregion

        #region Properties

        public IList<ManagedElevator> Elevators { get; }

        #endregion

        #region Specifications

        public Specification<ElevatorControl> GetAtLeastOneElevatorSpecification() 
        {
            return new AtLeastOneElevatorSpecification(Elevators);
        }

        #endregion
    }
}
