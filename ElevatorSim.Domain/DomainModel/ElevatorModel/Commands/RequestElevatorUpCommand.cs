using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class RequestElevatorUpCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public RequestElevatorUpCommand(ElevatorId aggregateId)
            : base(aggregateId)
        {

        }

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
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
