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
            string elevatorId,
            uint floorNumber,
            uint numberOfPeople)
            : base(aggregateId)
        {
            ElevatorId = elevatorId;
            FloorNumber = floorNumber;
            NumberOfPeople = numberOfPeople;
        }

        #endregion

        #region Properties

        public string ElevatorId { get; }

        public uint FloorNumber { get; }

        public uint NumberOfPeople { get; }

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
            aggregate.AssignElevator(new AssignedElevetor(
                command.ElevatorId,
                command.FloorNumber,
                command.NumberOfPeople
                ));

            return Task.FromResult(ExecutionResult.Success());
        }

        #endregion
    }
}
