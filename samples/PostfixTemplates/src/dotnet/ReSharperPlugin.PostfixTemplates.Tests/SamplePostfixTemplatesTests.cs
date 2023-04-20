using JetBrains.ReSharper.FeaturesTestFramework.Completion;
using NUnit.Framework;

namespace ReSharperPlugin.PostfixTemplates.Tests;

public class SamplePostfixTemplatesTests : CodeCompletionTestBase
{
    protected override string RelativeTestDataPath => nameof(SamplePostfixTemplatesTests);
    protected override CodeCompletionTestType TestType => CodeCompletionTestType.Action;

    [Test] public void TestIf01() { DoNamedTest(); }
}
