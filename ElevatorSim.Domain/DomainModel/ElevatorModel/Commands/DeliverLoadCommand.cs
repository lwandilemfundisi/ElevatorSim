using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class DeliverLoadCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public DeliverLoadCommand(
            ElevatorId aggregateId,
            DeliverLoad loadToDeliver)
            : base(aggregateId)
        {
            LoadToDeliver = loadToDeliver;
        }

        public DeliverLoad LoadToDeliver { get; }

        #endregion
    }

    public class DeliverLoadCommandHandler
        : CommandHandler<Elevator, ElevatorId, DeliverLoadCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate, 
            DeliverLoadCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.DeliverLoad(command.LoadToDeliver);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
