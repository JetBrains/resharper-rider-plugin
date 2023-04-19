using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharperPlugin.CodeInspections;

[ZoneMarker]
public class ZoneMarker : IRequire<ILanguageCSharpZone> { }
