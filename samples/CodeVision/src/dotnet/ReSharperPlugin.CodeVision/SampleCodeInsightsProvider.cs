using System.Collections.Generic;
using JetBrains.Application.Parts;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.CodeInsights;
using JetBrains.Rider.Model;
using JetBrains.Util;

namespace ReSharperPlugin.CodeVision;

[SolutionComponent(Instantiation.ContainerAsyncPrimaryThread)]
public class SampleCodeInsightsProvider : ICodeInsightsProvider
{
    public bool IsAvailableIn(ISolution solution)
    {
        return true;
    }

    public void OnClick(CodeInsightHighlightInfo highlightInfo, ISolution solution)
    {
        MessageBox.ShowInfo($"{nameof(SampleCodeInsightsProvider)}.{nameof(OnClick)}", "ReSharper SDK");
    }

    public void OnExtraActionClick(CodeInsightHighlightInfo highlightInfo, string actionId, ISolution solution)
    {
        MessageBox.ShowInfo($"{nameof(SampleCodeInsightsProvider)}.{nameof(OnExtraActionClick)}", "ReSharper SDK");
    }

    public string ProviderId => nameof(SampleCodeInsightsProvider);
    public string DisplayName => $"ReSharper SDK: {nameof(SampleCodeInsightsProvider)}.{nameof(DisplayName)}";

    public CodeVisionAnchorKind DefaultAnchor => CodeVisionAnchorKind.Top;

    public ICollection<CodeVisionRelativeOrdering> RelativeOrderings => new List<CodeVisionRelativeOrdering>
        { new CodeVisionRelativeOrderingFirst() };
}
