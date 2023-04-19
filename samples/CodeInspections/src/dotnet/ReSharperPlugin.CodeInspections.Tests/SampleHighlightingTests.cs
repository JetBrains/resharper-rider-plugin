using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace ReSharperPlugin.CodeInspections.Tests;

public class SampleHighlightingTests : CSharpHighlightingTestBase
{
    protected override string RelativeTestDataPath => nameof(SampleHighlightingTests);

    protected override bool HighlightingPredicate(
        IHighlighting highlighting,
        IPsiSourceFile sourceFile,
        IContextBoundSettingsStore settingsStore)
    {
        return highlighting is SampleHighlighting;
    }

    [Test] public void Test01() => DoNamedTest();
    [Test] public void Test02() => DoNamedTest();
}
