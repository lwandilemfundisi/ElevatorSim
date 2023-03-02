using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Jobs;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Jobs;
using Microservice.Framework.Domain.Subscribers;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class AssignedElevatorEventSubscriber
        : ISubscribeAsynchronousTo<ElevatorControl, ElevatorControlId, AssignedElevatorEvent>
    {
        private readonly IJobScheduler _jobScheduler;

        #region Constructors

        public AssignedElevatorEventSubscriber(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        #endregion

        #region Virtual Methods

        public Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, AssignedElevatorEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            var job = new AssignElevatorJob(
                domainEvent.AggregateIdentity,
                domainEvent.AggregateEvent.AssignedElevetor.ElevatorId,
                domainEvent.AggregateEvent.AssignedElevetor.FloorNumber,
                domainEvent.AggregateEvent.AssignedElevetor.NumberOfPeople);

            return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
        }

        #endregion
    }
}
