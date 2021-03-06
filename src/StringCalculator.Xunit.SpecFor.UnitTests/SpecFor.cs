﻿using Xunit;

namespace StringCalculator.Xunit.SpecFor.UnitTests
{
    public abstract class SpecFor<T>
    {
        private bool _initialized;

        protected T Subject;

        protected abstract T Given();

        protected abstract void When();

        public void Run()
        {
            if (_initialized != false) 
                return;

            Subject = Given();
            When();

            _initialized = true;
        }

        protected void CheckExists(object value)
        {
            Assert.NotNull(value);
        }

        protected void CheckValue<TValue>(TValue expectedValue, TValue actualValue)
        {
            Assert.Equal(expectedValue, actualValue);
        }
    }

    public class ThenAttribute : FactAttribute
    {   
    }
}
