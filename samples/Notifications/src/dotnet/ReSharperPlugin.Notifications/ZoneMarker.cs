using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharperPlugin.Notifications;

[ZoneMarker]
public class ZoneMarker : IRequire<IProjectModelZone> { }
