using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StringCalculator.Xunit
{
    public class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization() 
            : base(new AutoNSubstituteCustomization())
        {

        }
    }
}