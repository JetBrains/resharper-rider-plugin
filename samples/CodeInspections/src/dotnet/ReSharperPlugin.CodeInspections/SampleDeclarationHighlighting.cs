using JetBrains.Diagnostics;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.CodeInspections;

[RegisterConfigurableSeverity(
    SeverityId,
    CompoundItemName: null,
    Group: HighlightingGroupIds.CodeSmell,
    Title: Message,
    Description: Description,
    DefaultSeverity: Severity.WARNING)]
[ConfigurableSeverityHighlighting(
    SeverityId,
    CSharpLanguage.Name,
    OverlapResolve = OverlapResolveKind.ERROR,
    OverloadResolvePriority = 0,
    ToolTipFormatString = Message)]
public class SampleDeclarationHighlighting : IHighlighting
{
    public const string SeverityId = "SampleDeclarationInspection"; // Appears in suppression comments
    public const string Message = $"ReSharper SDK: {nameof(SampleDeclarationHighlighting)}.{nameof(Message)}";
    public const string Description = $"ReSharper SDK: {nameof(SampleDeclarationHighlighting)}.{nameof(Description)}";

    public SampleDeclarationHighlighting(ICSharpDeclaration declaration)
    {
        Declaration = declaration;
    }

    public ICSharpDeclaration Declaration { get; }

    public bool IsValid()
    {
        return Declaration.IsValid();
    }

    public DocumentRange CalculateRange()
    {
        return Declaration.NameIdentifier.NotNull().GetHighlightingRange();
    }

    public string ToolTip => $"ReSharper SDK: {nameof(SampleDeclarationHighlighting)}.{nameof(Message)}";

    public string ErrorStripeToolTip => $"ReSharper SDK: {nameof(SampleDeclarationHighlighting)}.{nameof(ErrorStripeToolTip)}";
}
