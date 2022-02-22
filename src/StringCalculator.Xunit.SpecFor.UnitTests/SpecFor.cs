using System;
using AutoFixture;
using Xunit;

namespace StringCalculator.Xunit.SpecFor.UnitTests
{
    public abstract class SpecFor<T>
    {
        protected readonly IFixture Fixture;

        private bool _initialized;

        protected T Subject { get; private set; }

        protected SpecFor() : 
            this(() => new Fixture().Customize(new DefaultCustomization()))
        {
        }

        protected SpecFor(Func<IFixture> fixtureFactory)
        {
            if (fixtureFactory == null) 
                throw new ArgumentNullException(nameof(fixtureFactory));
            
            Fixture = fixtureFactory();
            
            // ReSharper disable once VirtualMemberCallInConstructor
            PreSetup();
            SetUp();
        }
        
        protected abstract T Given();

        protected abstract void When();
        
        protected virtual void PreSetup()
        {
        }

        private void SetUp()
        {
            if (_initialized) 
                return;

            Subject = Given();
            When();

            _initialized = true;
        }
        
        
    }

    public class ThenAttribute : FactAttribute
    {   
    }
}
