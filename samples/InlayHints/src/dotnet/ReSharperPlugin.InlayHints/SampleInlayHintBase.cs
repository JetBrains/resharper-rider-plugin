using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.InlayHints;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.UI.RichText;

namespace ReSharperPlugin.InlayHints;

public abstract class SampleInlayHintBase : IInlayHintWithDescriptionHighlighting
{
    public const string HighlightAttributeIdBase = nameof(SampleInlayHintBase);
    public const string HighlightAttributeGroupId = HighlightAttributeIdBase + "Group";

    private readonly DocumentOffset _offset;
    private readonly ITreeNode _node;

    protected SampleInlayHintBase(ITreeNode node, DocumentOffset offset)
    {
        _node = node;
        _offset = offset;
    }

    public bool IsValid()
    {
        return _node.IsValid();
    }

    public DocumentRange CalculateRange()
    {
        return new DocumentRange(_offset);
    }

    public RichText Description => $"ReSharper SDK: {nameof(SampleInlayHintBase)}.{nameof(Description)}";
    public string ToolTip => $"ReSharper SDK: {nameof(SampleInlayHintBase)}.{nameof(ToolTip)}";
    public string ErrorStripeToolTip => $"ReSharper SDK: {nameof(SampleInlayHintBase)}.{nameof(ErrorStripeToolTip)}";
}
