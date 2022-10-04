using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.Actions.MenuGroups;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.ProjectModel;
using JetBrains.Rider.Model;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

#pragma warning disable CS0612
[Action("Open CEF Tool Window")]
#pragma warning restore CS0612
public class OpenCefToolWindowAction : IExecutableAction,
    IInsertLast<MainMenuFeaturesGroup> // TODO: whats that? :O
{
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
        return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
        var solution = context.GetComponent<ISolution>();
        var model = solution.GetComponent<CefToolWindowModel>();
        model.ActivateToolWindow.Value = true;
    }
}
