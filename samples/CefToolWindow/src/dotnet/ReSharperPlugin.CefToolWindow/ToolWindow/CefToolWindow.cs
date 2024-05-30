using JetBrains.Application.UI.ToolWindowManagement;
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
            (_, _) => _mySolution.GetComponent<CefToolWindowModel>().ToolWindowContent.Value);
    }

    public void ShowToolWindow()
    {
        _myToolWindowClass.Show(activate: true);
    }
}
