using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace ReSharperPlugin.ContextActions.Tests;

public class SampleQuickFixExecuteTests : CSharpContextActionExecuteTestBase<SampleContextAction>
{
    protected override string ExtraPath => string.Empty;
    protected override string RelativeTestDataPath => nameof(SampleQuickFixExecuteTests);

    [Test] public void Test01() { DoNamedTest(); }
}