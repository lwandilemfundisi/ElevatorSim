using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class DisableElevatorCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public DisableElevatorCommand(ElevatorId aggregateId)
            : base(aggregateId)
        {

        }

        #endregion
    }

    public class DisableElevatorCommandHandler
        : CommandHandler<Elevator, ElevatorId, DisableElevatorCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate, 
            DisableElevatorCommand command, 
            CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
