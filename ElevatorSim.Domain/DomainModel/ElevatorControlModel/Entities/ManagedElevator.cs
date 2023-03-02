using Microservice.Framework.Domain;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities
{
    public class ManagedElevator
        : Entity<ManagedElevatorId>
    {
        #region Properties

        public string ElevatorId { get; set; }

        #endregion
    }
}
