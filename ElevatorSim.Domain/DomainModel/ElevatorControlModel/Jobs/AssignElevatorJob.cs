using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Jobs
{
    [JobVersion("AssignElevatorJob", 1)]
    public class AssignElevatorJob
        : IJob
    {
        #region Constructors

        public AssignElevatorJob(
            ElevatorControlId aggregateId, 
            string elevatorId,
            uint fromFloor,
            uint toFloor,
            uint toLoadPeople) 
        {
            AggregateId = aggregateId;
            ElevatorId = elevatorId;
            ToLoadPeople = toLoadPeople;
            FromFloor = fromFloor;
            ToFloor = toFloor;
        }

        #endregion

        #region Properties

        public ElevatorControlId AggregateId { get; }
        public string ElevatorId { get; }
        public uint FromFloor { get; }
        public uint ToFloor { get; }
        public uint ToLoadPeople { get; }

        #endregion

        #region Vitrual Methods

        public Task ExecuteAsync(
            IServiceProvider serviceProvider, 
            CancellationToken cancellationToken)
        {
            var commandBus = serviceProvider
                .GetService<ICommandBus>();

            return commandBus
                .PublishAsync(new MoveElevatorCommand(
                    AggregateId, 
                    ElevatorId,
                    FromFloor,
                    ToFloor,
                    ToLoadPeople), cancellationToken);
        }

        #endregion
    }
}
