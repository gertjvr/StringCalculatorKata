using NUnit.Core.Extensibility;
using Ploeh.AutoFixture.NUnit2.Addins;
using Ploeh.AutoFixture.NUnit2.Addins.Builders;

namespace StringCalculator.BDD.UnitTests
{
    [NUnitAddin(Name = Constants.AutoDataExtension)]
    public class LocalAddin : IAddin
    {
        public bool Install(IExtensionHost host)
        {
            var providers = host.GetExtensionPoint("TestCaseProviders");
            if (providers == null)
                return false;

            providers.Install(new AutoDataProvider());

            return true;
        }
    }
}
