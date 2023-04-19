using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Features.Internal.Resources;
using JetBrains.Util;

namespace ReSharperPlugin.Actions;

[Action(
    ResourceType: typeof(Resources),
    TextResourceName: nameof(Resources.SampleFrontendTestedActionText),
    // Icon must also be changed in frontend code
    Icon = typeof(FeaturesInternalThemedIcons.QuickStartToolWindow),
    IdeaShortcuts = new[] { "Shift+Control+T" },
    VsShortcuts = new[] { "Shift+Control+T" })]
public class SampleFrontendTestedAction : IExecutableAction
{
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
        return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
        var solution = context.GetData(ProjectModelDataConstants.SOLUTION)!;
        var file = solution.SolutionDirectory / "file.txt";
        file.WriteAllText("Hello");
    }
}
