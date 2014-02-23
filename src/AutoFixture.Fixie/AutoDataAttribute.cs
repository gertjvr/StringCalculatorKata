﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture.Kernel;

namespace Ploeh.AutoFixture.Fixie
{
    /// <summary>
    /// Provides auto-generated data specimens generated by AutoFixture as an extention to
    /// NUnit TestCase attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //[CLSCompliant(false)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute is the root of a potential attribute hierarchy.")]
    public class AutoDataAttribute : DataAttribute
    {
        private readonly IFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDataAttribute"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
        /// <see cref="Fixture"/>.
        /// </para>
        /// </remarks>
        public AutoDataAttribute()
            : this(new Fixture())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDataAttribute"/> class with an
        /// <see cref="IFixture"/> of the supplied type.
        /// </summary>
        /// <param name="fixtureType">The type of the composer.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="fixtureType"/> does not implement <see cref="IFixture"/>
        /// or does not have a default constructor.
        /// </exception>
        public AutoDataAttribute(Type fixtureType)
            : this(AutoDataAttribute.CreateFixture(fixtureType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDataAttribute"/> class with the
        /// supplied <see cref="IFixture"/>.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public AutoDataAttribute(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }

            _fixture = fixture;
        }

        /// <summary>
        /// Gets the fixture used by <see cref="GetData"/> to create specimens.
        /// </summary>
        public IFixture Fixture
        {
            get { return _fixture; }
        }

        /// <summary>
        /// Gets the type of <see cref="Fixture"/>.
        /// </summary>
        public Type FixtureType
        {
            get { return Fixture.GetType(); }
        }

        /// <summary>
        /// Returns the data to be used to test the testcase.
        /// </summary>
        /// <param name="method">The method that is being tested</param>
        /// <returns>The testcase data generated by <see cref="Fixture"/>.</returns>
        public override IEnumerable<object[]> GetData(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            var specimens = new List<object>();
            foreach (var p in method.GetParameters())
            {
                CustomizeFixture(p);

                var specimen = Resolve(p);
                specimens.Add(specimen);
            }

            return new[] { specimens.ToArray() };
        }

        private void CustomizeFixture(ParameterInfo p)
        {
            var dummy = false;
            var customizeAttributes = p.GetCustomAttributes(typeof(CustomizeAttribute), dummy).OfType<CustomizeAttribute>();
            foreach (var ca in customizeAttributes)
            {
                var c = ca.GetCustomization(p);
                Fixture.Customize(c);
            }
        }

        private object Resolve(ParameterInfo p)
        {
            var context = new SpecimenContext(Fixture);
            return context.Resolve(p);
        }

        private static IFixture CreateFixture(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!typeof(IFixture).IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} is not compatible with IFixture. Please supply a Type which implements IFixture.",
                        type),
                    "type");
            }

            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} has no default constructor. Please supply a a Type that implements IFixture and has a default constructor. Alternatively you can supply an IFixture instance through one of the AutoTestCaseAttribute constructor overloads. If used as an attribute, this can be done from a derived class.",
                        type),
                    "type");
            }

            return (IFixture)ctor.Invoke(null);
        }
    }
}
