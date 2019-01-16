using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.SamplePlugin
{
    // Types mentioned in this attribute are used for performance optimizations
    [ElementProblemAnalyzer(
        typeof (ICSharpDeclaration),
        HighlightingTypes = new [] {typeof (SampleHighlighting)})]
    public class SampleProblemAnalyzer : ElementProblemAnalyzer<ICSharpDeclaration>
    {
        protected override void Run(ICSharpDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            // Hint: Avoid LINQ methods to increase performance
            // if (element.Name.Any(char.IsUpper))
            //     consumer.AddHighlighting(new IdentifierHasUpperCaseLetterHighlighting(element));

            // Hint: Also foreach creates additional enumerator
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < element.NameIdentifier?.Name.Length; i++)
            {
                if (!char.IsUpper(element.NameIdentifier.Name[i])) continue;
                
                consumer.AddHighlighting(new SampleHighlighting(element));
                return;
            }
        }
    }
}