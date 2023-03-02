using AutoFixture;
using AutoFixture.AutoMoq;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using Moq;

namespace ElevatorSim.Tests.Helpers
{
    public abstract class Test
    {
        protected IFixture _fixture { get; private set; }

        [SetUp]
        public void Setup() 
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Customize<ElevatorControlId>(x => x.FromFactory(() => ElevatorControlId.New));
            _fixture.Customize<ElevatorId>(x => x.FromFactory(() => ElevatorId.New));
        }

        protected T A<T>()
        {
            return _fixture.Create<T>();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            _fixture.Inject(mock.Object);
            return mock;
        }
    }
}
