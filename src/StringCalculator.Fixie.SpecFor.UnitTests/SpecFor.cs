namespace StringCalculator.Fixie.SpecFor.UnitTests
{
    public interface ISpecFor
    {
        
    }

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
