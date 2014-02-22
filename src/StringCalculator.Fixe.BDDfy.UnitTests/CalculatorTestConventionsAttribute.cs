using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using StringCalculator.Fixe.BDDfy.UnitTests.AutoFixture;

namespace StringCalculator.Fixe.BDDfy.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute() 
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
