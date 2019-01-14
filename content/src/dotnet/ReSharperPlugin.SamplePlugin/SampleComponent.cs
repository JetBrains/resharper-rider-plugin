using JetBrains.Application;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;

namespace ReSharperPlugin.CyclomaticComplexity
{
    [ShellComponent]
    internal class SampleQuickFixRegistrarComponent
    {
        public SampleQuickFixRegistrarComponent(IQuickFixes table)
        {
            table.RegisterQuickFix<SampleHighlighting>(null, h => new SampleFix(h.Declaration), typeof(SampleFix));
        }
    }
}