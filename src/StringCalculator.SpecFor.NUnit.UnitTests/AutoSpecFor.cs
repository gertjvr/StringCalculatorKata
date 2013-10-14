using Ploeh.AutoFixture;

namespace StringCalculator.SpecFor.UnitTests
{
    public abstract class AutoSpecFor<T> : SpecFor<T>
    {
        protected IFixture Fixture;

        protected AutoSpecFor()
            : this(new Fixture())
        {
   
        }

        protected AutoSpecFor(IFixture fixture)
        {
            Fixture = fixture;
        }
    }
}