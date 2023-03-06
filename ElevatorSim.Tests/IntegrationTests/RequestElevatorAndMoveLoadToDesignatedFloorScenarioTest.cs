using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Tests.Helpers;

namespace ElevatorSim.Tests.IntegrationTests
{
    public class RequestElevatorAndMoveLoadToDesignatedFloorScenarioTest
        : Test
    {
        [Test]
        public async Task PositiveTest()
        {
            //Arrange
            var testElevatorId = A<ElevatorId>();
            var testElevatorControlId = A<ElevatorControlId>();
            var init = A<InitializeControl>(x => x.FromFactory(() => new InitializeControl(
                new List<ManagedElevator>
                {
                    A<ManagedElevator>(x=>x.With(c => c.ElevatorId, testElevatorId.Value))
                })));

            await InitializeElevatorAggregateAsync(testElevatorId, 1, 10);
            await InitializeElevatorControlAggregateAsync(testElevatorControlId, init);

            //Act
            var result = await _commandBus
                .PublishAsync(new RequestElevatorCommand(
                    testElevatorControlId, 
                    new RequestElevetor(2, 3)), CancellationToken.None);

            Thread.Sleep(150000);

        }
    }
}
