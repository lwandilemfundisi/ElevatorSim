using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Rules.Notifications;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications
{
    public class ElevatorMoveDownSpecification
        : Specification<Elevator>
    {
        protected readonly uint _floorMovingTo;

        #region Constructors

        public ElevatorMoveDownSpecification(uint floorMovingTo)
        {
            _floorMovingTo = floorMovingTo;
        }

        #endregion

        #region Virtual Methods

        protected override Notification IsNotSatisfiedBecause(Elevator obj)
        {
            if (obj.CurrentFloor < _floorMovingTo)
            {
                return Notification.Create(new Message
                {
                    Text = $"Invalid instruction! The elevator has been instructed to move down but " +
                    $"the floor passed is for going up!",
                    Severity = SeverityType.Critical
                });
            }

            return Notification.CreateEmpty();
        }

        #endregion
    }
}
