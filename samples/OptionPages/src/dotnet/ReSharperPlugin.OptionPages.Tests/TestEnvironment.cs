using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.OptionPages.Tests
{
    [ZoneDefinition]
    public class OptionPagesTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IOptionPagesZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<OptionPagesTestEnvironmentZone> { }

    [SetUpFixture]
    public class OptionPagesTestsAssembly : ExtensionTestEnvironmentAssembly<OptionPagesTestEnvironmentZone> { }
}
