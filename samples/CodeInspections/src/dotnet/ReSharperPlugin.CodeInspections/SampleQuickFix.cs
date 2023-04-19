using System;
using System.Collections.Generic;
using JetBrains.Application.Progress;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.CodeInspections;

public class SampleQuickFix : QuickFixBase
{
    private readonly ICSharpDeclaration _declaration;

    public SampleQuickFix(ICSharpDeclaration declaration)
    {
        _declaration = declaration;
    }

    public override string Text => Resources.SampleQuickFixText;

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return _declaration.IsValid();
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var declaredElement = _declaration.DeclaredElement.NotNull();

        var psiServices = _declaration.GetPsiServices();
        var references = new List<IReference>();
        psiServices.Finder.FindReferences(
            declaredElement,
            domain: psiServices.SearchDomainFactory.CreateSearchDomain(_declaration),
            consumer: references.ConsumeReferences(),
            NullProgressIndicator.Create());

        _declaration.SetName(_declaration.NameIdentifier.NotNull().Name.ToUpperInvariant());

        foreach (var reference in references)
            reference.BindTo(declaredElement);

        return null;
    }
}
