using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace StringCalculator.SpecFor.Fixie.UnitTests.AutoFixture
{
    /// <summary>
    /// An attribute that can be applied to parameters in an <see cref="IEnumerable{T}"/>-driven
    /// TestCase to indicate that the parameter value should be created using a constructor with one
    /// or more <see cref="AutoDataAttribute" /> arguments, if applicable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FavorEnumerablesAttribute : CustomizeAttribute
    {
        /// <summary>
        /// Gets a customization that associates a <see cref="EnumerableFavoringConstructorQuery"/>
        /// with the <see cref="Type"/> of the parameter.
        /// </summary>
        /// <param name="parameter">The parameter for which the customization is requested.</param>
        /// <returns>
        /// A customization that associates a <see cref="EnumerableFavoringConstructorQuery"/> with
        /// the <see cref="Type"/> of the parameter.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return new ConstructorCustomization(parameter.ParameterType, new EnumerableFavoringConstructorQuery());
        }
    }
}