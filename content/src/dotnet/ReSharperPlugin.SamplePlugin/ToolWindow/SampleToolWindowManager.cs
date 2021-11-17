using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [SolutionComponent]
    public class SampleToolWindowManager
    {
        public SampleToolWindowManager(
            ToolWindowManager toolWindowManager,
            SamplePluginModel model,
            ISolution solution,
            Lifetime lifetime,
            SampleToolWindowDescriptor toolWindowDescriptor)
        {
            model.ActivateToolWindow.Advise(lifetime,
                b =>
                {
                    if (!b)
                        return;

                    var toolWindow = new SampleToolWindow(
                        lifetime,
                        solution,
                        toolWindowManager,
                        toolWindowDescriptor);
                    toolWindow.ShowToolWindow();
                });
        }
    }
}