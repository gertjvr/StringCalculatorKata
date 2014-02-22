using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace StringCalculator.Extensions.UnitTests
{
    public interface IBuilderStrategy
    {
        
    }

    public class OmitAutoProperties : IBuilderStrategy
    {
        
    }

    public class Sample
    {
        public string A { get; set; }
    }

    public static class Ext
    {
        public static T Create<T>(this ISpecimenBuilder builder, IBuilderStrategy strategy)
        {
            return builder.Create<T>();
        }
    }

    public class BuilderTests
    {
        public void ShouldReturnCorrectResult()
        {
            var fixture = new Fixture();
            fixture.Build<Sample>()
                .With(p => p.A)
                .Do(p =>
                {
                    p.A = Guid.NewGuid().ToString();
                })
                .Create();

        }
    }
}
