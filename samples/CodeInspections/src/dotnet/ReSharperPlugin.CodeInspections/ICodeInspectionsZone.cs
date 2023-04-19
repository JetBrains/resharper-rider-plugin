using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharperPlugin.CodeInspections;

[ZoneDefinition]
// [ZoneDefinitionConfigurableFeature("Title", "Description", IsInProductSection: false)]
public interface ICodeInspectionsZone : IZone,
    IRequire<ILanguageCSharpZone>
{
}
