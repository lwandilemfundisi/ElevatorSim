using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Persistence;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Commands
{
    [Category("Unit")]
    public class LoadPeopleCommandTest : Test
    {
        [Test]
        public async Task TestLoadPeopleCommand_Positive()
        {
            //Arrange
            var testId = ElevatorId.New;
            var load = new Load(2);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            var result = await _commandBus
                .PublishAsync(new LoadPeopleCommand(testId, load), CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task TestLoadPeopleCommand_Negative()
        {
            //Arrange
            var testId = ElevatorId.New;
            var load = new Load(11);
            await InitializeElevatorAggregateAsync(testId, 1, 10);

            //Act
            AsyncTestDelegate act = () => _commandBus
                .PublishAsync(new LoadPeopleCommand(testId, load), CancellationToken.None);

            //Assert
            Assert.That(act, Throws.TypeOf<DomainError>());
        }
    }
}
