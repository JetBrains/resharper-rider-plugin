using JetBrains.ProjectModel;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.TextControl.DocumentMarkup.Adornments;

namespace ReSharperPlugin.InlayHints;

[SolutionComponent]
public class SampleAdornmentProvider : IHighlighterAdornmentProvider
{
    public bool IsValid(IHighlighter highlighter)
    {
        return highlighter.UserData is SampleInlayHintBase;
    }

    public IAdornmentDataModel CreateDataModel(IHighlighter highlighter)
    {
        return highlighter.UserData is SampleInlayHint hint
            ? new SampleAdornmentDataModel(hint.ParameterName)
            : null;
    }
}
