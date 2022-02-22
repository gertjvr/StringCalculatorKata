using System;
using AutoFixture;
using NUnit.Framework;

namespace StringCalculator.NUnit.SpecFor.UnitTests
{
    [TestFixture]
    public abstract class SpecFor<T> where T : class
    {
        protected IFixture Fixture { get; }

        protected T Subject { get; private set; } = default!;

        protected SpecFor() 
            : this(() => new Fixture().Customize(new DefaultCustomization()))
        {
        }

        protected SpecFor(Func<IFixture> fixtureFactory)
        {
            if (fixtureFactory == null) 
                throw new ArgumentNullException(nameof(fixtureFactory));

            Fixture = fixtureFactory();
        }

        protected abstract T Given();

        protected abstract void When();

        [OneTimeSetUp]
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
