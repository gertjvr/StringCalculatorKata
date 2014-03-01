using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit;

namespace StringCalculator.Xunit.BDDfy.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute() 
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
