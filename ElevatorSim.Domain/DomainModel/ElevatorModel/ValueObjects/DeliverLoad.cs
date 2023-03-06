using ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications;
using Microservice.Framework.Common;
using Microservice.Framework.Domain;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects
{
    public class DeliverLoad
        : ValueObject
    {
        #region Constructors

        public DeliverLoad(uint loadToDeliver) 
        {
            LoadToDeliver = loadToDeliver;
        }

        #endregion

        #region Properties

        public uint LoadToDeliver { get; }

        #endregion

        #region Specifications

        public ISpecification<Elevator> GetDeliverLoadSpecification()
        {
            return new DeliverLoadSpecification(LoadToDeliver);
        }

        #endregion
    }
}
