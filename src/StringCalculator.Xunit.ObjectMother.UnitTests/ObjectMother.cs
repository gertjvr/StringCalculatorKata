using System.Collections.Generic;
using AutoFixture;

namespace StringCalculator.Xunit.ObjectMother.UnitTests
{
    public static class ObjectMother
    {
        public static T Get<T>(IFixture? fixture = null)
        {
            fixture ??= new Fixture().Customize(new DefaultCustomization());
            return fixture.Create<T>();
        }

        public static IEnumerable<T> GetList<T>(IFixture? fixture = null)
        {
            fixture ??= new Fixture().Customize(new DefaultCustomization());
            return fixture.Create<Generator<T>>();
        }
    }
}
