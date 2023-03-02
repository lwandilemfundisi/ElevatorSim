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
        private readonly ElevatorControlId _aggregateId;
        private readonly string _elevatorId;
        private readonly uint _toFloor;

        #region Constructors

        public AssignElevatorJob(
            ElevatorControlId aggregateId, 
            string elevatorId,
            uint toFloor) 
        {
            _aggregateId = aggregateId;
            _elevatorId = elevatorId;
            _toFloor = toFloor;
        }

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
                    _aggregateId, 
                    _elevatorId, 
                    _toFloor), cancellationToken);
        }

        #endregion
    }
}
