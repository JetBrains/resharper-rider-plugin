using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.SettingsProvider.Tests
{
    [ZoneDefinition]
    public class SettingsProviderTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<ISettingsProviderZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<SettingsProviderTestEnvironmentZone> { }

    [SetUpFixture]
    public class SettingsProviderTestsAssembly : ExtensionTestEnvironmentAssembly<SettingsProviderTestEnvironmentZone> { }
}
