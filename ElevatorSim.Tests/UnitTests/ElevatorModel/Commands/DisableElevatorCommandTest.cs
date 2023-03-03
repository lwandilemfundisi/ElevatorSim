using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Domain.Exceptions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class DisableElevatorCommandTest : Test
    {
        [Test]
        public async Task TestDisableElevatorCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new DisableElevatorCommand(testId), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TestDisableElevatorCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;

            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new DisableElevatorCommand(testId), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
