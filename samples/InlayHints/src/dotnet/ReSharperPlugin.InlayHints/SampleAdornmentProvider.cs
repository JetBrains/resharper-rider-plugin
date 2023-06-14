using JetBrains.ProjectModel;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.TextControl.DocumentMarkup.IntraTextAdornments;

namespace ReSharperPlugin.InlayHints;

[SolutionComponent]
public class SampleAdornmentProvider : IHighlighterIntraTextAdornmentProvider
{
    public bool IsValid(IHighlighter highlighter)
    {
        return highlighter.UserData is SampleInlayHintBase;
    }

    public IIntraTextAdornmentDataModel CreateDataModel(IHighlighter highlighter)
    {
        return highlighter.UserData is SampleInlayHint hint
            ? new SampleAdornmentDataModel(hint.ParameterName)
            : null;
    }
}
