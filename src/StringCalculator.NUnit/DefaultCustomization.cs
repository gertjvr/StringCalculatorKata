using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StringCalculator.NUnit
{
    public class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization() : base(
            new AutoNSubstituteCustomization())
        {
        }
    }
}