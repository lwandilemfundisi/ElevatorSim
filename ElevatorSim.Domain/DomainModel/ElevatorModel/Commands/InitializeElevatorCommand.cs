using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class InitializeElevatorCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public InitializeElevatorCommand(
            ElevatorId aggregateId,
            uint floor,
            uint weightLimit
            )
            : base(aggregateId)
        {
            Floor = floor;
            WeightLimit = weightLimit;
        }

        #endregion

        #region Properties

        public uint Floor { get; }

        public uint WeightLimit { get; }

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
            aggregate.InitializeElevator(
                command.Floor, 
                command.WeightLimit);

            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
