using JetBrains.Diagnostics;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.CodeInspections;

[RegisterConfigurableSeverity(
    SeverityId,
    CompoundItemName: null,
    CompoundItemNameResourceType: null,
    CompoundItemNameResourceName: null,
    Group: HighlightingGroupIds.CodeSmell,
    Title: null,
    TitleResourceType: typeof(Resources),
    TitleResourceName: nameof(Resources.SampleHighlightingTitle),
    Description: null,
    DescriptionResourceType: typeof(Resources),
    DescriptionResourceName: nameof(Resources.SampleHighlightingDescription),
    DefaultSeverity: Severity.WARNING)]
[ConfigurableSeverityHighlighting(
    SeverityId,
    CSharpLanguage.Name,
    OverlapResolve = OverlapResolveKind.ERROR,
    OverloadResolvePriority = 0,
    ToolTipFormatStringResourceType = typeof(Resources),
    ToolTipFormatStringResourceName = nameof(Resources.SampleHighlightingToolTipFormat))]
public class SampleHighlighting : IHighlighting
{
    public const string SeverityId = "Sample"; // Appears in suppression comments

    public SampleHighlighting(ICSharpDeclaration declaration)
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

    public string ToolTip => Resources.SampleHighlightingToolTipFormat; // Can be used with string.Format
    public string ErrorStripeToolTip => Resources.SampleHighlightingErrorStripeToolTip;
}
