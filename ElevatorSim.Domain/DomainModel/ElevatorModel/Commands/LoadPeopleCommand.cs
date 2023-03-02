using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Commands
{
    public class LoadPeopleCommand
        : Command<Elevator, ElevatorId>
    {
        #region Constructors

        public LoadPeopleCommand(
            ElevatorId aggregateId,
            Load load)
            : base(aggregateId)
        {
            Load = load;
        }

        #endregion

        #region Properties

        public Load Load { get; }

        #endregion
    }

    public class LoadPeopleCommandHandler
        : CommandHandler<Elevator, ElevatorId, LoadPeopleCommand>
    {
        #region Virtual Methods

        public override Task<IExecutionResult> ExecuteAsync(
            Elevator aggregate, 
            LoadPeopleCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.LoadPeople(command.Load);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
