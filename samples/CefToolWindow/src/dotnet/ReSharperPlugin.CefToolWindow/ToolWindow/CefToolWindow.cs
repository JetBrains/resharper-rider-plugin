using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Application.UI.UIAutomation;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rider.Model;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

public class CefToolWindow
{
    private readonly Lifetime _myLifetime;
    private readonly ISolution _mySolution;
    private readonly ToolWindowClass _myToolWindowClass;

    public CefToolWindow(
        Lifetime lifetime,
        ISolution solution,
        ToolWindowClass toolWindowClass)
    {
        _myLifetime = lifetime;
        _mySolution = solution;
        _myToolWindowClass = toolWindowClass;

        InitToolWindow();
    }

    private void InitToolWindow()
    {
        if (!_mySolution.GetComponent<ISolutionOwner>().IsRealSolutionOwner)
            return;

        _myToolWindowClass.RegisterInstance(_myLifetime,
            title: null,
            icon: null,
            (lifetime, _) =>
            {
                var model = _mySolution.GetComponent<CefToolWindowModel>();
                return EitherControl.FromAutomation(lifetime, model.ToolWindowContent.Value);
            });
    }

    public void ShowToolWindow()
    {
        _myToolWindowClass.Show(activate: true);
    }
}
