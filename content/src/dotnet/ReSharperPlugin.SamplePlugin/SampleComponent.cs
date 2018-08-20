using JetBrains.ProjectModel;
#if RIDER
using JetBrains.ReSharper.Host.Features;
using JetBrains.Rider.Model;
//using ReSharperPlugin.SamplePlugin.Rider.Model;
#endif

namespace ReSharperPlugin.SamplePlugin
{
    [SolutionComponent]
    public class SampleComponent
    {
        public SampleComponent(ISolution solution)
        {
            
            System.Diagnostics.Debugger.Launch();
#if RESHARPER
#elif RIDER
            var model = solution.GetProtocolSolution().GetSamplePluginModel();
#endif
        }
    }
}