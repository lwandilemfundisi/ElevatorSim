using Microservice.Framework.Common;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects
{
    [ValueObjectResourcePath("ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects.Mappings.ElevatorStatus.xml")]
    public class ElevatorStatus
        : XmlValueObject
    {
    }

    public class ElevatorStatuses
        : XmlValueObjectLookup<ElevatorStatus, ElevatorStatuses>
    {
        #region ValueObjects

        public ElevatorStatus InReady { get { return FindValueObject("INR"); } }

        public ElevatorStatus InOperation { get { return FindValueObject("IOP"); } }

        public ElevatorStatus InLoading { get { return FindValueObject("ILD"); } }

        public ElevatorStatus Disabled { get { return FindValueObject("DIS"); } }

        #endregion
    }
}
