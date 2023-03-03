using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Rules.Notifications;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications
{
    public class ElevatorMoveUpSpecification
        : Specification<Elevator>
    {
        protected readonly uint _floorMovingTo;

        #region Constructors

        public ElevatorMoveUpSpecification(uint floorMovingTo)
        {
            _floorMovingTo = floorMovingTo;
        }

        #endregion

        #region Virtual Methods

        protected override Notification IsNotSatisfiedBecause(Elevator obj)
        {
            if (obj.CurrentFloor > _floorMovingTo)
            {
                return Notification.Create(new Message
                {
                    Text = $"Invalid instruction! The elevator has been instructed to move up but " +
                    $"the floor passed is for going down!",
                    Severity = SeverityType.Critical
                });
            }

            return Notification.CreateEmpty();
        }

        #endregion
    }
}
