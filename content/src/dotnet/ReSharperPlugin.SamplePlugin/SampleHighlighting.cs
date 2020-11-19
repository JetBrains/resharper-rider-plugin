using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using ReSharperPlugin.SamplePlugin;

namespace ReSharperPlugin.SamplePlugin
{
    [RegisterConfigurableSeverity(
        SampleHighlighting.SeverityId,
        CompoundItemName: null,
        Group: HighlightingGroupIds.CodeSmell,
        Title: SampleHighlighting.Message,
        Description: SampleHighlighting.Description,
        DefaultSeverity: Severity.WARNING)]
    [ConfigurableSeverityHighlighting(
        SeverityId,
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.ERROR,
        OverloadResolvePriority = 0,
        ToolTipFormatString = Message)]
    public class SampleHighlighting : IHighlighting
    {
        public const string SeverityId = nameof(SampleHighlighting);
        public const string Message = nameof(SampleHighlighting) + " message";
        public const string Description = nameof(SampleHighlighting) + " description";
        
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
            return Declaration.NameIdentifier?.GetHighlightingRange() ?? DocumentRange.InvalidRange;
        }

        public string ToolTip => Message;
        
        public string ErrorStripeToolTip
            => $"Sample highlighting error tooltip for '{Declaration.DeclaredName}'";
    }
}
