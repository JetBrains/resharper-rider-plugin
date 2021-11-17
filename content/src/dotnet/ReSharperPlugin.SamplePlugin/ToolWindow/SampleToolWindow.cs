using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Application.UI.UIAutomation;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;

namespace ReSharperPlugin.SamplePlugin.ToolWindow
{
    [SolutionComponent]
    public class SampleToolWindow
    {
        private readonly Lifetime _myLifetime;
        private readonly ISolution _mySolution;
        private readonly ToolWindowClass _myToolWindowClass;

        public SampleToolWindow(
            Lifetime lifetime,
            ISolution solution,
            ToolWindowManager toolWindowManager,
            SampleToolWindowDescriptor descriptor)
        {
            _myLifetime = lifetime;
            _mySolution = solution;
            _myToolWindowClass = toolWindowManager.Classes[descriptor];

            InitToolWindow();
        }

        private void InitToolWindow()
        {
            if (!_mySolution.GetComponent<ISolutionOwner>().IsRealSolutionOwner)
                return;

            _myToolWindowClass.RegisterInstance(_myLifetime,
                title: null,
                icon: null,
                (lt, twi) =>
                {
                    var model = _mySolution.GetComponent<SamplePluginModel>();
                    return EitherControl.FromAutomation(model.ToolWindowContent.Value);
                });
        }

        public void ShowToolWindow()
        {
            _myToolWindowClass.Show(activate: true);
        }
    }
}