using JetBrains.Application.UI.Icons.CommonThemedIcons;
using JetBrains.Application.UI.Options.Options.ThemedIcons;
using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Collections.Viewable;
using JetBrains.Diagnostics;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Base;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.ExternalSources.Resources;
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
    public const string Schema = "resharper-plugin";
    public const string BaseUrl = $"{Schema}://home";

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
        var panel = new BeCefToolWindowPanel(url: $"{BaseUrl}/index.html", html: null);
        var panelWithToolBar = panel
            .InToolbar()
            .AddItem(
                BeControls.GetButton(
                        onClick: () => { panel.OpenUrl.Fire($"{BaseUrl}/index.html"); },
                        icon: iconHost.Transform(AssemblyExplorerThemedIcons.FolderResourcesClosed.Id),
                        lifetime: lifetime)
                    .WithCustomTooltip(title: "Load embedded web page", text: string.Empty))
            .AddItem(
                BeControls.GetButton(
                        onClick: () => { panel.ShowMessage("Web page alert from IDE!"); },
                        icon: iconHost.Transform(CommonThemedIcons.MessageInfo.Id),
                        lifetime: lifetime)
                    .WithCustomTooltip(title: "Send message to web page", text: string.Empty))
            .AddItem(
                BeControls.GetButton(
                        onClick: () => panel.OpenUrl.Fire("https://google.com"),
                        icon: iconHost.Transform(BrowsersThemedIcons.BrowserSystemDefault.Id),
                        lifetime: lifetime)
                    .WithCustomTooltip(title: "Load google.com", text: string.Empty))
            .AddItem(
                BeControls.GetComboBox(
                    lifetime,
                    values: new[] { "first", "second", "third" }))
            .AddItem(
                BeControls.GetToggleButton(
                    tooltip: "Toggle CEF Dev Tools",
                    onClick: value => panel.OpenDevTools.Fire(value),
                    icon: iconHost.Transform(OptionsThemedIcons.Options.Id),
                    lifetime: lifetime));

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

    public static byte[] GetResource(string request)
    {
        var assembly = typeof(CefToolWindowManager).Assembly;
        var path = request.TrimFromStart(BaseUrl).Trim('/');
        var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}");
        return stream.ReadAllBytes();
    }
}
