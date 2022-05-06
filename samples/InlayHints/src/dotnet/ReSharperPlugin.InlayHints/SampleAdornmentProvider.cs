using JetBrains.ProjectModel;
using JetBrains.TextControl.DocumentMarkup;

namespace ReSharperPlugin.CodeVision;

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
