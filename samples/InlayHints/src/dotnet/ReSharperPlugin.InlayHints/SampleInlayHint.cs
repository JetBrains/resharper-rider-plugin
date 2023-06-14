using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.InlayHints;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.TextControl.DocumentMarkup.VisualStudio;

namespace ReSharperPlugin.InlayHints;

[RegisterHighlighterGroup(
    HighlightAttributeGroupId,
    HighlightAttributeIdBase,
    HighlighterGroupPriority.CODE_SETTINGS)]
[RegisterHighlighter(
    HighlightAttributeId,
    GroupId = HighlightAttributeGroupId,
    ForegroundColor = "#707070",
    BackgroundColor = "#EBEBEB",
    DarkForegroundColor = "#787878",
    DarkBackgroundColor = "#3B3B3C",
    EffectType = EffectType.INTRA_TEXT_ADORNMENT,
    Layer = HighlighterLayer.ADDITIONAL_SYNTAX,
    VsGenerateClassificationDefinition = VsGenerateDefinition.VisibleClassification,
    VsBaseClassificationType = VsPredefinedClassificationType.Text,
    TransmitUpdates = true)]
[DaemonIntraTextAdornmentProvider(typeof(SampleAdornmentProvider))]
[DaemonTooltipProvider(typeof(InlayHintTooltipProvider))]
[StaticSeverityHighlighting(Severity.INFO, typeof(HighlightingGroupIds.CodeInsights), AttributeId = HighlightAttributeId)]
public class SampleInlayHint : SampleInlayHintBase
{
    public string ParameterName { get; }
    public const string HighlightAttributeId = nameof(SampleInlayHint);

    public SampleInlayHint(string parameterName, ITreeNode node, DocumentOffset offset)
        : base(node, offset)
    {
        ParameterName = parameterName;
    }
}
