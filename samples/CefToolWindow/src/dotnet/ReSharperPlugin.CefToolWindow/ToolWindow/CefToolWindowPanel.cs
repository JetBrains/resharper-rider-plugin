#if RESHARPER && WIN
using System;
using System.Linq;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using JetBrains.Application.UI.Automation;
using JetBrains.Lifetimes;
using JetBrains.PsiFeatures.VisualStudio.SinceVs10.NuGet.NuGetBrowser.Services;
using JetBrains.Rider.Model;
using JetBrains.UI.SrcView.Automation;

namespace ReSharper.Nuke.ToolWindow
{
    [View]
    public class CefToolWindowPanel : ViewControl<BeCefToolWindowPanel>
    {
        protected override UIElement OnRenderView(Lifetime lifetime, BeCefToolWindowPanel automation)
        {
            var browser = new ChromiumWebBrowser(automation.Url);
            lifetime.Bracket(
                () => browser.LoadingStateChanged += OnLoadingStateChanged,
                () => browser.LoadingStateChanged -= OnLoadingStateChanged);

            lifetime.AddDispose(browser);

            return browser;
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
        }
    }
}
#endif
