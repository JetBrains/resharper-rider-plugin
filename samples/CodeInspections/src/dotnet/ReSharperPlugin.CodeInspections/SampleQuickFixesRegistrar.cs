using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.Util;

namespace ReSharperPlugin.CodeInspections;

[ShellComponent]
internal class SampleQuickFixesRegistrar : IQuickFixesProvider
{
    public void Register(IQuickFixesRegistrar registrar)
    {
        registrar.RegisterQuickFix<SampleHighlighting>(Lifetime.Eternal, h => new SampleQuickFix(h.Declaration), typeof(SampleQuickFix));
    }

    public IEnumerable<Type> Dependencies => EmptyArray<Type>.Instance;
}
