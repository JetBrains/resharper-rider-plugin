using JetBrains.Application;
using JetBrains.Application.BuildScript.Application;
using JetBrains.Application.UI.Icons.CommonThemedIcons;
using JetBrains.Application.UI.ToolWindowManagement;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

[ToolWindowDescriptor(
    Icon = typeof(CommonThemedIcons.Bulb),
    InstancePresentableName = $"{nameof(ToolWindowDescriptor)}.{nameof(ToolWindowDescriptorAttribute.InstancePresentableName)}",
    ProductNeutralId = $"{nameof(ToolWindowDescriptor)}.{nameof(ToolWindowDescriptorAttribute.ProductNeutralId)}",
    Text = $"{nameof(ToolWindowDescriptor)}.{nameof(ToolWindowDescriptorAttribute.Text)}",
    Type = ToolWindowType.SingleInstance,
    VisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Solution)]
public class CefToolWindowDescriptor : ToolWindowDescriptor
{
    public CefToolWindowDescriptor(IApplicationHost host, IWindowBranding branding)
        : base(host, branding)
    {
    }
    //
    // public CefToolWindowDescriptor(IApplicationHost host)
    //     : base(host)
    // {
    // }
}
