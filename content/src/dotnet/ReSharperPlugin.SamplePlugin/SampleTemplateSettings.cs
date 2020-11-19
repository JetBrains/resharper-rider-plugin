// Copyright 2019 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/resharper/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;

namespace ReSharperPlugin.SamplePlugin
{
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
