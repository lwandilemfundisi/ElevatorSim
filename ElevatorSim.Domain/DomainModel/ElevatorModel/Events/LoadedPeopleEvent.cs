using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Events
{
    [EventVersion("LoadedPeopleEvent", 1)]
    public class LoadedPeopleEvent
        : AggregateEvent<Elevator, ElevatorId>
    {
        #region Constructors

        public LoadedPeopleEvent(uint numberOfPeople) 
        {
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public uint NumberOfPeople { get; }

        #endregion
    }
}
