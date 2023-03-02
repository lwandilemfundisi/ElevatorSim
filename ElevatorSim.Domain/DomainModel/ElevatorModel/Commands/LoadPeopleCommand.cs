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
            uint numberOfPeople)
            : base(aggregateId)
        {
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public uint NumberOfPeople { get; }

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
            aggregate.LoadPeople(command.NumberOfPeople);
            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
