using System.IO;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Settings;

namespace ReSharperPlugin.SettingsProvider;

[DefaultSettings(typeof (LiveTemplatesSettings))] // TODO: update with proper settings key type
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
