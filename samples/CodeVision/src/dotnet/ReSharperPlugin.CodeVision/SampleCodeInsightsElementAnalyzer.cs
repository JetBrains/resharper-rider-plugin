using JetBrains.ReSharper.Daemon.CodeInsights;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
#if RIDER
using JetBrains.ReSharper.Features.Internal.Resources;
using JetBrains.Rider.Backend.Platform.Icons;
#endif

namespace ReSharperPlugin.CodeVision;

#if RIDER
[ElementProblemAnalyzer(
    typeof(ICSharpFunctionDeclaration))]
#endif
public class SampleCodeInsightsElementAnalyzer : ElementProblemAnalyzer<ICSharpFunctionDeclaration>
{
#if RIDER
    private readonly SampleCodeInsightsProvider _codeInsightsProvider;
    private readonly IconHost _iconHost;

    public SampleCodeInsightsElementAnalyzer(
        SampleCodeInsightsProvider codeInsightsProvider,
        IconHost iconHost)
    {
        _codeInsightsProvider = codeInsightsProvider;
        _iconHost = iconHost;
    }
#endif

    protected override void Run(
        ICSharpFunctionDeclaration element,
        ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
#if RIDER
        consumer.AddHighlighting(
            new CodeInsightsHighlighting(
                element.GetNameDocumentRange(),
                displayText: "ReSharper SDK: displayText",
                tooltipText: "ReSharper SDK: tooltipText",
                moreText: "ReSharper SDK: moreText",
                _codeInsightsProvider,
                element.DeclaredElement,
                _iconHost.Transform(FeaturesInternalThemedIcons.QuickStartToolWindow.Id))
        );
#endif
    }
}
