using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.QuickDocProvider.Tests
{
    [ZoneDefinition]
    public class QuickDocProviderTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IQuickDocProviderZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<QuickDocProviderTestEnvironmentZone> { }

    [SetUpFixture]
    public class QuickDocProviderTestsAssembly : ExtensionTestEnvironmentAssembly<QuickDocProviderTestEnvironmentZone> { }
}
