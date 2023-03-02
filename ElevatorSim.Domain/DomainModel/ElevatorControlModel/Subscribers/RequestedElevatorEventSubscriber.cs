using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Jobs;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Jobs;
using Microservice.Framework.Domain.Subscribers;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class RequestedElevatorEventSubscriber
        : ISubscribeAsynchronousTo<ElevatorControl, ElevatorControlId, RequestedElevatorEvent>
    {
        private readonly IJobScheduler _jobScheduler;

        #region Constructors

        public RequestedElevatorEventSubscriber(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        #endregion

        #region Virtual Methods

        public Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, RequestedElevatorEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            var job = new AssignElevatorJob(
                domainEvent.AggregateIdentity,
                string.Empty,
                domainEvent.AggregateEvent.RequestElevetor.FloorNumber,
                domainEvent.AggregateEvent.RequestElevetor.NumberOfPeople);

            return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
        }

        #endregion
    }
}
