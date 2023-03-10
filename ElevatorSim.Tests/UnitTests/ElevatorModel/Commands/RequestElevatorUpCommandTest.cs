using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Domain.Exceptions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class RequestElevatorUpCommandTest : Test
    {
        [Test]
        public async Task TestRequestElevatorUpCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(null, 2, 1, false);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new RequestElevatorUpCommand(testId, move), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task TestRequestElevatorUpCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(null, 1, 1, false);
            await InitializeElevatorAggregateAsync(testId, 2, 10);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new RequestElevatorUpCommand(testId, move), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
