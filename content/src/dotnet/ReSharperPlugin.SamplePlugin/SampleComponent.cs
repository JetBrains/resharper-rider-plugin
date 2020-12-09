using JetBrains.Application;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;

namespace ReSharperPlugin.SamplePlugin
{
    [ShellComponent]
    internal class SampleQuickFixRegistrarComponent
    {
        public SampleQuickFixRegistrarComponent(IQuickFixes table)
        {
            // This connects an inspection with an actual quick-fix
            table.RegisterQuickFix<SampleHighlighting>(
                Lifetime.Eternal,
                h => new SampleFix(h.Declaration),
                typeof(SampleFix));
        }
    }
}
