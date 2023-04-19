using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace ReSharperPlugin.CodeInspections.Tests;

public class SampleQuickFixTests : CSharpQuickFixTestBase<SampleQuickFix>
{
    protected override string RelativeTestDataPath => nameof(SampleQuickFixTests);

    [Test] public void Test01() { DoNamedTest(); }
}