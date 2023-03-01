using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class RequestElevatorDownCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public RequestElevatorDownCommand(ElevatorId aggregateId)
            : base(aggregateId)
        {

        }

        #endregion
    }

    public class RequestElevatorDownCommandHandler
        : CommandHandler<Elevator, ElevatorId, RequestElevatorDownCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate, 
            RequestElevatorDownCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.MoveDown();
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
