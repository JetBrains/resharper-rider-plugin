using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.CefToolWindow.Tests
{
    [ZoneDefinition]
    public class CefToolWindowTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<ICefToolWindowZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<CefToolWindowTestEnvironmentZone> { }

    [SetUpFixture]
    public class CefToolWindowTestsAssembly : ExtensionTestEnvironmentAssembly<CefToolWindowTestEnvironmentZone> { }
}
