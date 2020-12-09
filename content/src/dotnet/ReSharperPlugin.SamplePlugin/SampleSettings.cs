using JetBrains.Application.Settings;
using JetBrains.Application.Settings.WellKnownRootKeys;

namespace ReSharperPlugin.SamplePlugin
{
    // Settings that can persist in dotSettings files
    [SettingsKey(
        typeof(EnvironmentSettings),
//        typeof(CodeEditingSettings),
        "Settings for SamplePlugin")]
    public class SampleSettings
    {
        [SettingsEntry(DefaultValue: "<default>", Description: "Sample Description")]
        public string SampleText;
    }
}
