using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.RdProtocol.Tests
{
    [ZoneDefinition]
    public class RdProtocolTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IRdProtocolZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<RdProtocolTestEnvironmentZone> { }

    [SetUpFixture]
    public class RdProtocolTestsAssembly : ExtensionTestEnvironmentAssembly<RdProtocolTestEnvironmentZone> { }
}
