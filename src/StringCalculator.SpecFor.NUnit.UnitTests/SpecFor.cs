using NUnit.Framework;

namespace StringCalculator.SpecFor.UnitTests
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

        protected void CheckExists(object value)
        {
            Assert.IsNotNull(value);
        }

        protected void CheckValue<TValue>(TValue expectedValue, TValue actualValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }
    }

    public class ThenAttribute : TestAttribute
    {   
    }
}
