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
            uint fromFloor,
            uint toFloor,
            uint toLoadPeople)
            : base(aggregateId)
        {
            FromFloor = fromFloor;
            ToFloor = toFloor;
            ElevatorId = elevatorId;
            ToLoadPeople = toLoadPeople;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }

        public uint FromFloor { get; }

        public uint ToFloor { get; }

        public uint ToLoadPeople { get; }

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
            aggregate.MoveElevator(
                command.ElevatorId,
                command.FromFloor,
                command.ToFloor, 
                command.ToLoadPeople);

            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
