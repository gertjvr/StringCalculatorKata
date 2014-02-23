using System;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace AutoFixture.Fixie
{
    /// <summary>
    /// An attribute that can be applied to parameters in an <see cref="IFixture"/>-driven
    /// TestCase to indicate that the parameter value should be created using the most greedy
    /// constructor that can be satisfied by an <see cref="AutoDataAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class GreedyAttribute : CustomizeAttribute
    {
        /// <summary>
        /// Gets a customization that associates a <see cref="GreedyConstructorQuery"/> with the
        /// <see cref="Type"/> of the parameter.
        /// </summary>
        /// <param name="parameter">The parameter for which the customization is requested.</param>
        /// <returns>
        /// A customization that associates a <see cref="GreedyConstructorQuery"/> with the
        /// <see cref="Type"/> of the parameter.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            return new ConstructorCustomization(parameter.ParameterType, new GreedyConstructorQuery());
        }
    }
}