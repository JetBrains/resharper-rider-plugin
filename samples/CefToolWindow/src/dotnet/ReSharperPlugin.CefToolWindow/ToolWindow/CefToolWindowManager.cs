using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Application.Resources;
using JetBrains.Application.UI.Icons.CommonThemedIcons;
using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Collections.Viewable;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Base;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Rider.Model;
using NuGet;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

[SolutionComponent]
public class CefToolWindowManager
{
    public CefToolWindowManager(
        ToolWindowManager toolWindowManager,
        CefToolWindowModel model,
        ISolution solution,
        IIconHost iconHost,
        Lifetime lifetime,
        CefToolWindowDescriptor toolWindowDescriptor)
    {
#if RESHARPER
        // CefSharp.CefRuntime.LoadCefSharpCoreRuntimeAnyCpu();
#endif
        var panel = new BeCefToolWindowPanel(url: "nuke:///index.html", html: null);
        var panelWithToolBar = panel
            .InToolbar()
            .AddItem(
                BeControls.GetButton(
                    iconHost.Transform(CommonThemedIcons.Refresh.Id),
                    lifetime,
                    onClick: () => panel.OpenDevTools.Fire(true)))
            .AddItem(
                BeControls.GetButton(
                    iconHost.Transform(BrowsersThemedIcons.BrowserChrome.Id),
                    lifetime,
                    onClick: () => panel.OpenUrl.Fire("https://google.com")))
            // .AddItem(
            //     BeControls.GetTextBox(
            //         lifetime,
            //         id: "textBox",
            //         initialText: "initial text"))
            .AddItem(
                BeControls.GetComboBox(
                    lifetime,
                    values: new[] { "first", "second", "third" }))
            .AddItem(
                BeControls.GetToggleButton(
                    iconHost.Transform(CommonThemedIcons.Pin.Id),
                    lifetime,
                    "Toggle Button",
                    onClick: _ => { }));
        panel.GetResource.Set((_, resourceName) =>
        {
            var assembly = typeof(CefToolWindowManager).Assembly;
            var path = new Uri(resourceName).AbsolutePath.TrimStart('/').Replace("/", ".");
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}");
            return Task.FromResult(stream.ReadToEnd()).ToRdTask();
        });
        model.ToolWindowContent.SetValue(panelWithToolBar);
        model.ActivateToolWindow.WhenTrue(lifetime,
            _ =>
            {
                var toolWindowClass = toolWindowManager.Classes[toolWindowDescriptor];
                var toolWindow = new CefToolWindow(lifetime, solution, toolWindowClass);
                toolWindow.ShowToolWindow();
            });
    }
}
