using NUnit.Core.Extensibility;
using Ploeh.AutoFixture.NUnit2.Addins;

namespace StringCalculator.UnitTests
{
    [NUnitAddin(Name = Constants.AutoDataExtension)]
    public class LocalAddin : Ploeh.AutoFixture.NUnit2.Addins.Addin
    {
    }
}
