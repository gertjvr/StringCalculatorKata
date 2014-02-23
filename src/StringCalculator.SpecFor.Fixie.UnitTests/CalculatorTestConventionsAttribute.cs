using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using StringCalculator.SpecFor.Fixie.UnitTests.AutoFixture;

namespace StringCalculator.SpecFor.Fixie.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
