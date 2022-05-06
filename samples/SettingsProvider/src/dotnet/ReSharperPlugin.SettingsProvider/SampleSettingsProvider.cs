using System.IO;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;

namespace ReSharperPlugin.SettingsProvider;

[ShellComponent]
public class SampleSettingsProvider : IHaveDefaultSettingsStream
{
    public string Name => "ReSharper SDK";

    public Stream GetDefaultSettingsStream(Lifetime lifetime)
    {
        var manifestResourceStream = typeof(SampleSettingsProvider).Assembly
            .GetManifestResourceStream(typeof(SampleSettingsProvider).Namespace + ".SampleSettings.DotSettings").NotNull();
        lifetime.OnTermination(manifestResourceStream);
        return manifestResourceStream;
    }
}
