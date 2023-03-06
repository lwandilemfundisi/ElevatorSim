using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Domain.Exceptions;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    public class DeliverLoadCommandTest
        : Test
    {
        [Test]
        public async Task TestDeliverLoadCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var deliverLoad = new DeliverLoad(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10, 2);

            //Act
            var result = await _commandBus
                .PublishAsync(new DeliverLoadCommand(testId, deliverLoad), CancellationToken.None);
            var assetAggregate = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(testId), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            assetAggregate.CurrentWeight.Should().Be(0);
            assetAggregate.ElevatorStatus.Should().Be(ElevatorStatuses.Of().InReady);
        }

        [Test]
        public async Task TestDeliverLoadCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var deliverLoad = new DeliverLoad(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10, 1);

            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new DeliverLoadCommand(testId, deliverLoad), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
