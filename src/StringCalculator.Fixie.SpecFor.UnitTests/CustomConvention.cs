using Fixie;
using Fixie.Conventions;
using Ploeh.AutoFixture.Fixie;

namespace StringCalculator.Fixie.SpecFor.UnitTests
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes
                .Where(t => typeof(ISpecFor).IsAssignableFrom(t));

            Methods
                .Where(method => method.IsVoid() && (method.Name.EndsWith("ReturnsCorrectResult") || method.Name.EndsWith("ThrowsCorrectException")));

            ClassExecution
                .CreateInstancePerTestClass();

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }
    }
}