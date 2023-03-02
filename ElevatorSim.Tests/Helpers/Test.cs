using AutoFixture;
using AutoFixture.AutoMoq;

namespace ElevatorSim.Tests.Helpers
{
    public abstract class Test
    {
        protected Fixture _fixture;

        [SetUp]
        public void Setup() 
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        protected T A<T>()
        {
            return _fixture.Create<T>();
        }
    }
}
