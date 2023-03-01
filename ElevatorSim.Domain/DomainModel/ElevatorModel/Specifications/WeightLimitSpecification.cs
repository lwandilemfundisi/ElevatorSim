using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Rules.Notifications;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications
{
    public class WeightLimitSpecification
        : Specification<Elevator>
    {
        protected readonly uint _weight;

        #region Constructors

        public WeightLimitSpecification(uint weight)
        {
            _weight = weight;
        }

        #endregion

        #region Virtual Methods

        protected override Notification IsNotSatisfiedBecause(Elevator obj)
        {
            if(_weight > obj.Weightlimit)
            {
                return Notification.Create(new Message
                {
                    Text = $"Elevator has exceeded its certified weight limit to safely operate!",
                    Severity = SeverityType.Critical
                });
            }

            return Notification.CreateEmpty();
        }

        #endregion
    }
}
