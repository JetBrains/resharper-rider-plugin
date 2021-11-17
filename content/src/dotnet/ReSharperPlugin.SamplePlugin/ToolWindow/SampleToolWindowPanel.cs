#if WIN
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Application.UI.Automation;
using JetBrains.Lifetimes;
using JetBrains.UI.SrcView.Automation;
using ReSharperPlugin.SamplePlugin;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [View]
    public class NukeToolWindowPanel : ViewControl<BeSampleToolWindowPanel>
    {
        protected override UIElement OnRenderView(Lifetime lifetime, BeSampleToolWindowPanel automation)
        {
            return null;
        }
    }
}
#endif