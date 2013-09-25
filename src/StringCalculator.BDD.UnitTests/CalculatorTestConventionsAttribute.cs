using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.NUnit2;

namespace StringCalculator.BDD.UnitTests
{
    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute() 
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
