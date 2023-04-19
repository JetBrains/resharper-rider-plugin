using System.Linq;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.CodeInspections;

// Types mentioned in this attribute are used for performance optimizations
[ElementProblemAnalyzer(
    typeof(ICSharpDeclaration),
    HighlightingTypes = new[] { typeof(SampleHighlighting) })]
public class SampleProblemAnalyzer : ElementProblemAnalyzer<ICSharpDeclaration>
{
    protected override void Run(
        ICSharpDeclaration element,
        ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
        if (element.NameIdentifier?.Name.All(char.IsUpper) ?? true)
            return;

        consumer.AddHighlighting(new SampleHighlighting(element));
    }
}
