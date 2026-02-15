using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperPlugin.SamplePlugin.Tests;

[ZoneMarker]
public class ZoneMarker : IRequire<SamplePluginTestEnvironmentZone>;
