using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using StringCalculator.BDDfy.Fixie.UnitTests.AutoFixture;

namespace StringCalculator.BDDfy.Fixie.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
