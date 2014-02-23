using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie;
using Fixie.Conventions;
using Ploeh.AutoFixture.Fixie;

namespace StringCalculator.SpecFor.Fixie.UnitTests
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
                .CreateInstancePerTestClass()
                .SortCases((caseA, caseB) => String.Compare(caseA.Name, caseB.Name, StringComparison.Ordinal));

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }
    }
}