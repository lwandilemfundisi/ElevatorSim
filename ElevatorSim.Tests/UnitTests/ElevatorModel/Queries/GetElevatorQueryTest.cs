using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Queries;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Common;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Queries
{
    [Category("Unit")]
    public class GetElevatorQueryTest : Test
    {
        [Test]
        public async Task TestGetElevatorQuery_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _queryProcessor
                .ProcessAsync(new GetElevatorQuery(testId), CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(testId);
            result.CurrentFloor.Should().Be(1);
            result.Weightlimit.Should().Be(10);
        }

        [Test]
        public async Task TestGetElevatorQuery_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            AsyncTestDelegate act = () => _queryProcessor
                .ProcessAsync(new GetElevatorQuery(null), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<InvariantException>());
        }
    }
}
