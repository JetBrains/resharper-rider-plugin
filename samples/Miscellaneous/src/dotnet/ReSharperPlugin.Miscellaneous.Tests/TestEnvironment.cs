using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.Miscellaneous.Tests
{
    [ZoneDefinition]
    public class MiscellaneousTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IMiscellaneousZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<MiscellaneousTestEnvironmentZone> { }

    [SetUpFixture]
    public class MiscellaneousTestsAssembly : ExtensionTestEnvironmentAssembly<MiscellaneousTestEnvironmentZone> { }
}
