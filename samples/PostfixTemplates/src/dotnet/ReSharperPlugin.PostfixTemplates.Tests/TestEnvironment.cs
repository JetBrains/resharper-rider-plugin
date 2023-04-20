using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.PostfixTemplates.Tests
{
    [ZoneDefinition]
    public class PostfixTemplatesTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IPostfixTemplatesZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<PostfixTemplatesTestEnvironmentZone> { }

    [SetUpFixture]
    public class PostfixTemplatesTestsAssembly : ExtensionTestEnvironmentAssembly<PostfixTemplatesTestEnvironmentZone> { }
}
