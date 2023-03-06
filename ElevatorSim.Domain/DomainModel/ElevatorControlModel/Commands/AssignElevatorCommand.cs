using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands
{
    public class AssignElevatorCommand
        : Command<ElevatorControl, ElevatorControlId>
    {
        #region Constructors

        public AssignElevatorCommand(
            ElevatorControlId aggregateId,
            AssignedElevator assignedElevetor)
            : base(aggregateId)
        {
            AssignedElevetor = assignedElevetor;
        }

        #endregion

        #region Properties

        public AssignedElevator AssignedElevetor { get; }

        #endregion
    }

    public class AssignElevatorCommandHandler
        : CommandHandler<ElevatorControl, ElevatorControlId, AssignElevatorCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            ElevatorControl aggregate, 
            AssignElevatorCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.AssignElevator(command.AssignedElevetor);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
