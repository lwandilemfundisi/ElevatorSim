using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands
{
    public class RequestElevatorCommand
        : Command<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public RequestElevatorCommand(ElevatorControlId aggregateId)
            : base(aggregateId)
        {
        }

        #endregion
    }

    public class RequestElevatorCommandHandler
        : CommandHandler<ElevatorControl, ElevatorControlId, RequestElevatorCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            ElevatorControl aggregate, 
            RequestElevatorCommand command, 
            CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
