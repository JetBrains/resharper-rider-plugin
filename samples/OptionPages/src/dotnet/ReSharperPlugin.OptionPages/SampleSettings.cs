using JetBrains.Application.Settings;
using JetBrains.Application.Settings.WellKnownRootKeys;
using JetBrains.Util;
using NuGet;

namespace ReSharperPlugin.OptionPages;

[SettingsKey(
    // Discover others through usages of SettingsKeyAttribute
    Parent: typeof(EnvironmentSettings),
    Description: "ReSharper SDK – Sample Settings")]
public class SampleSettings
{
    [SettingsEntry(DefaultValue: "Default text", Description: "Private description")]
    public string String;

    [SettingsEntry(DefaultValue: 42, Description: "Private description")]
    public int Integer;

    [SettingsEntry(DefaultValue: true, Description: "Private description")]
    public bool Boolean;

    [SettingsEntry(DefaultValue: SettingsEnum.Second, Description: "Private description")]
    public SettingsEnum RadioSelection;

    [SettingsEntry(DefaultValue: SettingsEnum.Third, Description: "Private description")]
    public SettingsEnum ComboSelection;

    [SettingsEntry(DefaultValue: default(string), Description: "Private description")]
    public string FolderPath;
}

public enum SettingsEnum
{
    First,
    Second,
    Third
}
