using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.Specifications;
using ElevatorSim.Tests.Helpers;
using FluentAssertions;

namespace ElevatorSim.Tests.UnitTests.ElevatorModel.Specifications
{
    [Category("Unit")]
    public class WeightLimitSpecificationTests : Test
    {
        [Test]
        public void Valid()
        {
            //Arrange
            var sut = new WeightLimitSpecification(2);
            var testAggregate = new Elevator() { Weightlimit = 2};

            //Act
            var isSatisfiedBy = sut.IsSatisfiedBy(testAggregate);
            var why = sut.WhyIsNotSatisfiedBy(testAggregate);

            //Assert
            isSatisfiedBy.Should().BeTrue();
            why.Should().HaveCount(0);
        }

        [Test]
        public void Invalid()
        {
            //Arrange
            var sut = new WeightLimitSpecification(6);
            var testAggregate = new Elevator() { Weightlimit = 2 };

            //Act
            var isSatisfiedBy = sut.IsSatisfiedBy(testAggregate);
            var why = sut.WhyIsNotSatisfiedBy(testAggregate);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }
    }
}
