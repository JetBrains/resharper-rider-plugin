using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Application.Resources;
using JetBrains.Application.UI.Icons.CommonThemedIcons;
using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Collections.Viewable;
using JetBrains.Diagnostics;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Base;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Rider.Model;
using JetBrains.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                    onClick: () => { panel.ShowMessage("foooooobar!"); }))
            .AddItem(
                BeControls.GetButton(
                    iconHost.Transform(CommonThemedIcons.Duplicate.Id),
                    lifetime,
                    onClick: () => { panel.OpenUrl.Fire("nuke:///index.html"); }))
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
                    onClick: value => panel.OpenDevTools.Fire(value)));

        panel.MessageReceived.Advise(lifetime, message =>
        {
            var obj = JsonConvert.DeserializeObject<JObject>(message).NotNull();
            var value = (obj["data"]?.Value<string>()).NotNull();
            MessageBox.ShowInfo(value);
        });

        panel.GetResource.SetSync(GetResource);

        model.ActivateToolWindow.WhenTrue(lifetime, _ =>
        {
            var toolWindowClass = toolWindowManager.Classes[toolWindowDescriptor];
            var toolWindow = new CefToolWindow(lifetime, solution, toolWindowClass);
            toolWindow.ShowToolWindow();
        });

        model.ToolWindowContent.SetValue(panelWithToolBar);
    }

    public static string GetResource(string request)
    {
        var assembly = typeof(CefToolWindowManager).Assembly;
        var path = new Uri(request).AbsolutePath.TrimStart('/').Replace("/", ".");
        var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}");
        return stream.ReadToEnd();
    }
}
