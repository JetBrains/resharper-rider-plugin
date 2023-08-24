#if RESHARPER
using System;
using System.IO;
using System.Text;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using JetBrains.Application.Threading;
using JetBrains.Application.UI.Automation;
using JetBrains.Core;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model;
using JetBrains.UI.SrcView.Automation;
using JetBrains.Util.Logging;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

[View]
public class CefToolWindowPanel : ViewControl<BeCefToolWindowPanel>
{
    protected override UIElement OnRenderView(Lifetime lifetime, BeCefToolWindowPanel automation)
    {
        var assemblyDirectory = Path.GetDirectoryName(typeof(CefToolWindowPanel).Assembly.Location).NotNull();
        var executable = Path.Combine(assemblyDirectory, "CefSharp.BrowserSubprocess.exe");
        var settings = new CefSettings { BrowserSubprocessPath = executable };
        settings.RegisterScheme(new CefCustomScheme
        {
            SchemeName = CefToolWindowManager.Schema,
            SchemeHandlerFactory = new ResourceSchemaHandler()
        });
        Cef.Initialize(settings); // on UI thread!

        var browser = new ChromiumWebBrowser(automation.Url);
        lifetime.Bracket(
            () => browser.LoadingStateChanged += OnLoadingStateChanged,
            () => browser.LoadingStateChanged -= OnLoadingStateChanged);

        lifetime.AddDispose(browser);

        automation.OpenDevTools.Advise(lifetime, _ => browser.ShowDevTools());
        automation.OpenUrl.Advise(lifetime, x => browser.Load(x));

        // Web -> ReSharper
        browser.LoadingStateChanged += (_, _) =>
        {
            var script = """
                if (!messageSentEvent) {
                    var messageSentEvent = new Event('messageSentEvent');
                    document.addEventListener('messageSentEvent', (message) => {
                        CefSharp.PostMessage(JSON.stringify(message.detail));
                    });
                }
                """;

            browser.GetMainFrame().ExecuteJavaScriptAsync(script);
        };

        var threading = Shell.Instance.GetComponent<IThreading>();
        browser.JavascriptMessageReceived += (_, args) =>
        {
            var message = args.ConvertMessageTo<string>();
            threading.ExecuteOrQueue(
                lifetime,
                $"{nameof(CefToolWindowPanel)}.{nameof(ChromiumWebBrowser.JavascriptMessageReceived)}",
                () => automation.MessageReceived.Fire(message));
        };

        // ReSharper -> Web
        automation.SendMessage.SetSync(message =>
        {
            var dispatchMessage = $"document.dispatchEvent({message})";
            browser.GetMainFrame().ExecuteJavaScriptAsync(dispatchMessage);
            return Unit.Instance;
        });

        return browser;
    }

    private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
    {
    }
}

public class ResourceSchemaHandler : ISchemeHandlerFactory
{
    public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
    {
        try
        {
            var resource = CefToolWindowManager.GetResource(request.Url);
            var stream = new MemoryStream(resource);
            return ResourceHandler.FromStream(stream);
        }
        catch (Exception ex)
        {
            Logger.GetLogger<ResourceSchemaHandler>().Error(ex);
            return null;
        }
    }
}
#endif
