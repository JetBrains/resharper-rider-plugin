using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Application.UI.Options.OptionPages;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Util;

namespace ReSharperPlugin.CodeInspections;

[ShellComponent]
internal class SampleQuickFixesRegistrar : IQuickFixesProvider
{
    public void Register(IQuickFixesRegistrar registrar)
    {
        registrar.RegisterQuickFix<SampleDeclarationHighlighting>(Lifetime.Eternal, h => new SampleDeclarationQuickFix(h.Declaration), typeof(SampleDeclarationQuickFix));
    }

    public IEnumerable<Type> Dependencies => EmptyArray<Type>.Instance;
}
