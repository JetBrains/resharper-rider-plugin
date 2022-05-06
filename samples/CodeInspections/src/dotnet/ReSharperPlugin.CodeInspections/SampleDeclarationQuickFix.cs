using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.CodeInspections;

public class SampleDeclarationQuickFix : QuickFixBase
{
    private readonly ICSharpDeclaration _declaration;

    public SampleDeclarationQuickFix(ICSharpDeclaration declaration)
    {
        _declaration = declaration;
    }

    public override string Text => "ReSharper SDK: Symbol name to upper-case";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return _declaration.IsValid() && _declaration is IMethodDeclaration;
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
