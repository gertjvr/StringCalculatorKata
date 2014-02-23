namespace StringCalculator.SpecFor.Fixie.UnitTests
{
    public abstract class SpecFor<T> : ISpecFor
    {
        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        public void SetUp()
        {
            Subject = Given();
            When();
        }
    }
}