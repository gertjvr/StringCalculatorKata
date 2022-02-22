using AutoFixture;
using AutoFixture.NUnit3;

namespace StringCalculator.NUnit
{
    public class UseAutoFixtureToCreateParametersAttribute : AutoDataAttribute
    {
        public UseAutoFixtureToCreateParametersAttribute() 
            : base(() => new Fixture().Customize(new DefaultCustomization()))
        {
        }
    }
}
