using AutoFixture;
using AutoFixture.Xunit2;

namespace StringCalculator.Xunit
{
    public class UseAutoFixtureToCreateParametersAttribute : AutoDataAttribute
    {
        public UseAutoFixtureToCreateParametersAttribute() 
            : base(() => new Fixture().Customize(new DefaultCustomization()))
        {
        }
    }
}
