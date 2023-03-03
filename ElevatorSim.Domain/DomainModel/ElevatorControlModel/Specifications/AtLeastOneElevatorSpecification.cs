using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Rules.Notifications;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Specifications
{
    public class AtLeastOneElevatorSpecification
        : Specification<ElevatorControl>
    {
        private readonly IList<ManagedElevator> _elevators;

        #region Constructors

        public AtLeastOneElevatorSpecification(IList<ManagedElevator> elevators)
        {
            _elevators = elevators;
        }

        #endregion

        #region Constructors

        protected override Notification IsNotSatisfiedBecause(ElevatorControl obj)
        {
            if(!_elevators.Any()) 
            {
                return Notification.Create(new Message
                {
                    Text = $"Please add at least one elevator to control",
                    Severity = SeverityType.Critical
                });
            }

            return Notification.CreateEmpty();
        }

        #endregion
    }
}
