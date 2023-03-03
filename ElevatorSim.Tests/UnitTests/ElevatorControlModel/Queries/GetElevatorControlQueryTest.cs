using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Queries;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Tests.Helpers;
using Microservice.Framework.Common;

namespace ElevatorSim.Tests.UnitTests.ElevatorControlModel.Queries
{
    [Category("Unit")]
    public class GetElevatorControlQueryTest : Test
    {
        [Test]
        public async Task TestGetElevatorControlQuery_Positive()
        {
            //Arrange
            var testId = A<ElevatorControlId>();
            var init = A<InitializeControl>();
            await InitializeElevatorControlAggregateAsync(testId, init);

            //Act
            var result = await _queryProcessor
                .ProcessAsync(new GetElevatorControlQuery(testId), CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(testId);
            result.Elevators.HasItems().Should().BeTrue();
        }

        [Test]
        public void TestGetElevatorControlQuery_Negative()
        {
            //Act
            AsyncTestDelegate act = () => _queryProcessor
                .ProcessAsync(new GetElevatorControlQuery(null), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<InvariantException>());
        }
    }
}
