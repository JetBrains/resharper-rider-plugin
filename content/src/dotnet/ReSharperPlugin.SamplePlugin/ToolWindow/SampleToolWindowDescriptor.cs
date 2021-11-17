using JetBrains.Application;
using JetBrains.Application.BuildScript.Application;
using JetBrains.Application.UI.Icons.CommonThemedIcons;
using JetBrains.Application.UI.ToolWindowManagement;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [ToolWindowDescriptor(
        Icon = typeof(CommonThemedIcons.Success),
        InstancePresentableName = "sample",
        ProductNeutralId = "SampleToolWindow",
        Text = "Sample Tool Window",
        Type = ToolWindowType.SingleInstance,
        VisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Global)]
    public class SampleToolWindowDescriptor : ToolWindowDescriptor
    {
        public SampleToolWindowDescriptor(IApplicationHost host, IWindowBranding branding)
            : base(host, branding)
        {
        }

        public SampleToolWindowDescriptor(IApplicationHost host)
            : base(host)
        {
        }
    }
}