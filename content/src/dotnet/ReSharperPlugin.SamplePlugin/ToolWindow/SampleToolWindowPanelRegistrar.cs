using JetBrains.Application.Threading;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Features.Inspections.Resources;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [SolutionComponent]
    public class SampleToolWindowPanelRegistrar
    {
        public SampleToolWindowPanelRegistrar(
            Lifetime lifetime,
            IPsiServices services,
            ISolution solution,
            IIconHost iconHost,
            IThreading threading,
            SamplePluginModel model)
        {
            var nukeModelPanel = new BeSampleToolWindowPanel("args");

            var toggleButton = BeControls.GetToggleButton(
                CallHierarchyThemedIcons.CalledByArrow.Id.GetIcon(iconHost),
                lifetime,
                "Sample Toggle Button",
                onClick: b => { });

            model.ToolWindowContent.Value =
                nukeModelPanel.InToolbar()
                    .AddItem(toggleButton);
        }
    }
}