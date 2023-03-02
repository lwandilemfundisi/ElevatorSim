using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands
{
    public class MoveElevatorCommand
        : Command<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public MoveElevatorCommand(
            ElevatorControlId aggregateId, 
            string elevatorId,
            uint toFloor)
            : base(aggregateId)
        {
            ToFloor = toFloor;
            ElevatorId = elevatorId;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }

        public uint ToFloor { get; }

        #endregion

    }

    public class MoveElevatorCommandHandler
        : CommandHandler<ElevatorControl, ElevatorControlId, MoveElevatorCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            ElevatorControl aggregate, 
            MoveElevatorCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.MoveElevator(command.ElevatorId, command.ToFloor);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
