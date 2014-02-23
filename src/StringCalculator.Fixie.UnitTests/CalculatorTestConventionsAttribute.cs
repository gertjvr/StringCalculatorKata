using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using StringCalculator.Fixie.UnitTests.AutoFixture;

namespace StringCalculator.Fixie.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}