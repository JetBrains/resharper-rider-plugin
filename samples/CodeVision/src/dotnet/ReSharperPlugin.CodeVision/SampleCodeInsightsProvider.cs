using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.CodeInsights;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model;
using JetBrains.Util;

namespace ReSharperPlugin.CodeVision;

[SolutionComponent]
public class SampleCodeInsightsProvider : ICodeInsightsProvider
{
    public bool IsAvailableIn(ISolution solution)
    {
        return true;
    }

    public void OnClick(CodeInsightsHighlighting highlighting, ISolution solution)
    {
        MessageBox.ShowInfo($"{nameof(SampleCodeInsightsProvider)}.{nameof(OnClick)}", "ReSharper SDK");
    }

    public void OnExtraActionClick(CodeInsightsHighlighting highlighting, string actionId, ISolution solution)
    {
        MessageBox.ShowInfo($"{nameof(SampleCodeInsightsProvider)}.{nameof(OnExtraActionClick)}", "ReSharper SDK");
    }

    public string ProviderId => nameof(SampleCodeInsightsProvider);
    public string DisplayName => $"ReSharper SDK: {nameof(SampleCodeInsightsProvider)}.{nameof(DisplayName)}";
    public CodeLensAnchorKind DefaultAnchor => CodeLensAnchorKind.Top;

    public ICollection<CodeLensRelativeOrdering> RelativeOrderings => new CodeLensRelativeOrdering[]
        {new CodeLensRelativeOrderingFirst()};
}
