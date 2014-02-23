using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie;
using Fixie.Conventions;
using StringCalculator.SpecFor.Fixie.UnitTests.AutoFixture;

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

            Parameters(GetData);

            ClassExecution
                .CreateInstancePerTestClass()
                .SortCases((caseA, caseB) => String.Compare(caseA.Name, caseB.Name, StringComparison.Ordinal));

            InstanceExecution
                .SetUpTearDown("FixtureSetUp", "FixtureTearDown");

            CaseExecution
                .SetUpTearDown("SetUp", "TearDown");
        }

        private IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var data = (DataAttribute)methodInfo.GetCustomAttributes(typeof(DataAttribute), true).FirstOrDefault();

            if (data == null)
                return new List<object[]>();

            return data.GetData(methodInfo);
        }
    }
}