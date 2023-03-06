using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Tests.Helpers;

namespace ElevatorSim.Tests.UnitTests.ElevatorControlModel.Commands
{
    [Category("Unit")]
    public class AssignElevatorCommandTest : Test
    {
        [Test]
        public async Task TestAssignElevatorCommand_Positive()
        {
            //Arrange
            var testId = A<ElevatorControlId>();
            var init = A<InitializeControl>();
            var elevatorId = init.Elevators.FirstOrDefault().ElevatorId;
            await InitializeElevatorControlAggregateAsync(testId, init);

            //Act
            var result = await _commandBus
                .PublishAsync(new AssignElevatorCommand(
                    testId, 
                    new AssignedElevator(elevatorId, 1, 2, 3)), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
