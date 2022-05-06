using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.CodeInspections;

[ContextAction(
    Group = CSharpContextActions.GroupID,
    Name = $"ReSharper SDK: {nameof(SampleContextAction)}.{nameof(ContextActionAttribute.Name)}",
    Description = $"ReSharper SDK: {nameof(SampleContextAction)}.{nameof(ContextActionAttribute.Description)}",
    Priority = -10)]
public class SampleContextAction : ContextActionBase
{
    private readonly ICSharpContextActionDataProvider _provider;

    public SampleContextAction(ICSharpContextActionDataProvider provider)
    {
        _provider = provider;
    }

    public override string Text => $"ReSharper SDK: {nameof(SampleContextAction)}.{nameof(Text)}";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return _provider.GetSelectedElement<ICSharpFunctionDeclaration>() != null;
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var declaration = _provider.GetSelectedElement<ICSharpFunctionDeclaration>();
        if (declaration == null)
            return null;

        using (WriteLockCookie.Create())
        {
            var elementFactory = CSharpElementFactory.GetInstance(declaration);
            var commentToken = elementFactory.CreateComment("// ReSharper SDK");
            commentToken = ModificationUtil.AddChildBefore(declaration, commentToken);

            return x => x.Caret.MoveTo(commentToken.GetDocumentEndOffset(), CaretVisualPlacement.DontScrollIfVisible);
        }
    }
}