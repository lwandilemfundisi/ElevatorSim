using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Domain.Exceptions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class RequestElevatorDownCommandTest : Test
    {
        [Test]
        public async Task TestRequestElevatorDownCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(1);
            await InitializeElevatorAggregateAsync(testId, 2, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new RequestElevatorDownCommand(testId, move), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task TestRequestElevatorDownCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var move = new Move(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new RequestElevatorDownCommand(testId, move), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
