using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Rules.Notifications;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications
{
    public class DeliverLoadSpecification
        : Specification<Elevator>
    {
        protected readonly uint _loadToDeliver;

        #region Constructors

        public DeliverLoadSpecification(uint loadToDeliver)
        {
            _loadToDeliver = loadToDeliver;
        }

        #endregion

        #region Virtual Methods

        protected override Notification IsNotSatisfiedBecause(Elevator obj)
        {
            if (obj.CurrentWeight < _loadToDeliver)
            {
                return Notification.Create(new Message
                {
                    Text = $"Something went wrong! Current weight cannot be less than load to deliver!",
                    Severity = SeverityType.Critical
                });
            }

            return Notification.CreateEmpty();
        }

        #endregion
    }
}
