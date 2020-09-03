using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperPlugin.SamplePlugin
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<ISamplePluginZone>
    {
    }
}