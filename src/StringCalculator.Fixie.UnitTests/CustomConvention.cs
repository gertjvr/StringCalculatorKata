﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie;
using Fixie.Conventions;
using StringCalculator.Fixie.UnitTests.AutoFixture;

namespace StringCalculator.Fixie.UnitTests
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes
                .NameEndsWith("Tests");

            Methods
                .Where(method => method.IsVoid());
                //.Where(m => m.Name.EndsWith("ReturnsCorrectResult"))
                //.Where(m => m.Name.EndsWith("ThrowsCorrectException"));

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
            var data = (DataAttribute) methodInfo.GetCustomAttributes(typeof (DataAttribute), true).FirstOrDefault();

            if (data == null)
                return new List<object[]>();

            return data.GetData(methodInfo);
        }
    }
}