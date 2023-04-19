using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace ReSharperPlugin.ContextActions.Tests;

public class SampleQuickFixAvailabilityTests : CSharpContextActionAvailabilityTestBase<SampleContextAction>
{
    protected override string ExtraPath => string.Empty;
    protected override string RelativeTestDataPath => nameof(SampleQuickFixAvailabilityTests);

    [Test] public void Test01() { DoNamedTest(); }
}
