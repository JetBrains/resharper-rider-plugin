using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.Actions.MenuGroups;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.ProjectModel;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [Action("SampleOpenToolWindow", "Open Sample Tool Window")]
    public class SampleOpenToolWindowAction : IExecutableAction,
        IInsertLast<MainMenuFeaturesGroup> // TODO: whats that? :O
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var solution = context.GetComponent<ISolution>();
            var nukeModel = solution.GetComponent<SamplePluginModel>();
            nukeModel.ActivateToolWindow.Value = true;
        }
    }
}