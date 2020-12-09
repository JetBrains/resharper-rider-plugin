using System;
using System.IO;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;

namespace ReSharperPlugin.SamplePlugin
{
    // Templates (or settings in general) that ship with the plugin
    [ShellComponent]
    public class SampleTemplateSettings : IHaveDefaultSettingsStream
    {
        public string Name => "SamplePlugin Template Settings";

        public Stream GetDefaultSettingsStream(Lifetime lifetime)
        {
            var manifestResourceStream = typeof(SampleTemplateSettings).Assembly
                .GetManifestResourceStream(typeof(SampleTemplateSettings).Namespace + ".Templates.DotSettings").NotNull();
            lifetime.OnTermination(manifestResourceStream);
            return manifestResourceStream;
        }
    }
}
