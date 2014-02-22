using NUnit.Framework;

namespace StringCalculator.NUnit.SpecFor.UnitTests
{
    [TestFixture]
    public abstract class SpecFor<T>
    {
        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        [TestFixtureSetUp]
        public void SetUp()
        {
            Subject = Given();
            When();
        }
    }

    public class ThenAttribute : TestAttribute
    {   
    }
}
