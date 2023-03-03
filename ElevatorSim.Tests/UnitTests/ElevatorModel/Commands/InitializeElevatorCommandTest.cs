using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class InitializeElevatorCommandTest : Test
    {
        [Test]
        public async Task TestInitializeElevatorCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;

            //Act
            var result = await _commandBus
                .PublishAsync(new InitializeElevatorCommand(testId, 1, 10), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
