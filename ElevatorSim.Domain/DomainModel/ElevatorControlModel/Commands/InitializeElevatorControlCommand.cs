using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands
{
    public class InitializeElevatorControlCommand
        : Command<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public InitializeElevatorControlCommand(
            ElevatorControlId aggregateId,
            InitializeControl initializeControl)
            : base(aggregateId)
        {
            InitializeControl = initializeControl;
        }

        #endregion

        #region Properties

        public InitializeControl InitializeControl { get; }

        #endregion
    }

    public class InitializeElevatorControlCommandHandler
        : CommandHandler<ElevatorControl, ElevatorControlId, InitializeElevatorControlCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            ElevatorControl aggregate, 
            InitializeElevatorControlCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.InitializeElevatorControl(command.InitializeControl);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
