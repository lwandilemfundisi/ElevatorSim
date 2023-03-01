using Microservice.Framework.Domain.Aggregates;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel
{
    public class Elevator 
        : AggregateRoot<Elevator, ElevatorId>
    {
        private Action<object, string> LazyLoader { get; set; }

        #region Constructors

        public Elevator()
            : base(null)
        {
        }

        public Elevator(ElevatorId id)
            : base(id)
        {
        }


        #endregion
    }
}
