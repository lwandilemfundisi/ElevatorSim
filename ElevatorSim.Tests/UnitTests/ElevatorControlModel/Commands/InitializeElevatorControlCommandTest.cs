using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Domain.Exceptions;

namespace ElevatorSim.Tests.UnitTests.ElevatorControlModel.Commands
{
    [Category("Unit")]
    public class InitializeElevatorControlCommandTest : Test
    {
        [Test]
        public async Task TestInitializeElevatorControlCommand_Positive()
        {
            //Arrange
            var testId = A<ElevatorControlId>();
            var init = A<InitializeControl>();

            //Act
            var result = await _commandBus
                .PublishAsync(new InitializeElevatorControlCommand(
                    testId,
                    init), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TestInitializeElevatorControlCommand_Negative()
        {
            //Arrange
            var testId = A<ElevatorControlId>();
            var init = new InitializeControl(null);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new InitializeElevatorControlCommand(
                    testId,
                    init), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
