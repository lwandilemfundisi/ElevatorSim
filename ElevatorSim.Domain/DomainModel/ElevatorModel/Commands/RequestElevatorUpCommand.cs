using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class RequestElevatorUpCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public RequestElevatorUpCommand(
            ElevatorId aggregateId,
            Move move)
            : base(aggregateId)
        {
            Move = move;
        }

        #endregion

        #region Properties

        public Move Move { get; }

        #endregion
    }

    public class RequestElevatorUpCommandHandler
        : CommandHandler<Elevator, ElevatorId, RequestElevatorUpCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate,
            RequestElevatorUpCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.MoveUp(command.Move);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
