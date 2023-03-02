using AutoFixture;

namespace ElevatorSim.Tests.Helpers
{
    public abstract class TestsFor<TSut> : Test
    {
        private Lazy<TSut> _lazySut;
        protected TSut Sut => _lazySut.Value;

        [SetUp]
        public void SetUpTestsFor()
        {
            _lazySut = new Lazy<TSut>(CreateSut);
        }

        protected virtual TSut CreateSut()
        {
            return _fixture.Create<TSut>();
        }
    }
}
