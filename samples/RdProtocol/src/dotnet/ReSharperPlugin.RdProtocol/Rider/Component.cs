using System;
using System.Threading.Tasks;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.RdBackend.Common.Features;
using ReSharperPlugin.RdProtocol;

namespace ReSharperPlugin.TestPlugin
{
    [SolutionComponent]
    public class Component
    {
        public Component(Lifetime lifetime, ISolution solution)
        {
            // Get the model
            var model = solution.GetProtocolSolution().GetRdProtocolModel();

            // Properties
            model.Property.Value = new CustomType(
                @string: "value",
                boolean: true,
                array: new[] { "first", "second" });

            model.Property.Advise(lifetime, x =>
            {
                // Handle changes
            });

            // Maps
            model.Map["key"] = "value";
            var mapValue = model.Map["key"];

            model.Map.Advise(lifetime, x =>
            {
                // Handle changes
                var mapKey = x.Key;
            });

            // Call frontend remote procedure
            model.Callback.Sync("args");

            // Set backend remote procedure
            model.Call.Set((lt, x) => Task.FromResult(new[] { x }).ToRdTask());

            // Trigger backend event
            model.Sink("value");

            // Subscribe frontend event
            model.Source.Advise(lifetime, x => { });
        
            // Trigger/subscribe bidirectional event
            model.Signal.Fire("value");
            model.Signal.Advise(lifetime, x => { });
        }
    }
}
