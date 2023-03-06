using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Events;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Jobs;
using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Jobs;
using Microservice.Framework.Domain.Subscribers;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Subscribers
{
    public class AssignedElevatorEventSubscriber
        : ISubscribeSynchronousTo<ElevatorControl, ElevatorControlId, AssignedElevatorEvent>
    {
        private readonly ILogger<AssignedElevatorEventSubscriber> _logger;
        private readonly IJobScheduler _jobScheduler;

        #region Constructors

        public AssignedElevatorEventSubscriber(
            IJobScheduler jobScheduler,
            ILogger<AssignedElevatorEventSubscriber> logger)
        {
            _logger = logger;
            _jobScheduler = jobScheduler;
        }

        #endregion

        #region Virtual Methods

        public Task HandleAsync(
            IDomainEvent<ElevatorControl, ElevatorControlId, AssignedElevatorEvent> domainEvent, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigned elevator!");

            var job = new AssignElevatorJob(
                domainEvent.AggregateIdentity,
                domainEvent.AggregateEvent.AssignedElevetor.ElevatorId,
                domainEvent.AggregateEvent.AssignedElevetor.FromFloorNumber,
                domainEvent.AggregateEvent.AssignedElevetor.ToFloorNumber,
                domainEvent.AggregateEvent.AssignedElevetor.NumberOfPeople);

            return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
        }

        #endregion
    }
}
