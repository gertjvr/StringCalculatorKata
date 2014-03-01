using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace StringCalculator.Fixie.ObjectMother.UnitTests
{
    public static class ObjectMother
    {
        public static T Get<T>(IFixture fixture = null)
        {
            fixture = fixture ?? new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());
            
            return fixture.Create<T>();
        }

        public static IEnumerable<T> GetList<T>(IFixture fixture = null)
        {
            fixture = fixture ?? new Fixture();

            return fixture.Create<Generator<T>>();
        }

    }
}
