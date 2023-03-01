using Microservice.Framework.Domain.Aggregates;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel
{
    public class ElevatorControl 
        : AggregateRoot<ElevatorControl, ElevatorControlId>
    {
        private Action<object, string> LazyLoader { get; set; }

        #region Constructors

        public ElevatorControl()
            : base(null)
        {
        }

        public ElevatorControl(ElevatorControlId id)
            : base(id)
        {
        }

        #endregion
    }
}
