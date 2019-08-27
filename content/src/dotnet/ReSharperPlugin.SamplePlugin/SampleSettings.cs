using JetBrains.Application.Settings;
using JetBrains.Application.Settings.WellKnownRootKeys;

namespace ReSharperPlugin.SamplePlugin
{
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