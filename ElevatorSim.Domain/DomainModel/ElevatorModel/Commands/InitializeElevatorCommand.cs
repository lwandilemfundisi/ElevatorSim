using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class InitializeElevatorCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public InitializeElevatorCommand(ElevatorId aggregateId)
            : base(aggregateId)
        {

        }

        #endregion
    }

    public class InitializeElevatorCommandHandler
        : CommandHandler<Elevator, ElevatorId, InitializeElevatorCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate, 
            InitializeElevatorCommand command, 
            CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
