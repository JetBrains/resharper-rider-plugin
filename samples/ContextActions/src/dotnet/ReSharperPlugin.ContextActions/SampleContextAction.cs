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

namespace ReSharperPlugin.ContextActions;

[ContextAction(
    Group = CSharpContextActions.GroupID,
    ResourceType = typeof(Resources),
    NameResourceName = nameof(Resources.SampleContextActionName),
    DescriptionResourceName = nameof(Resources.SampleContextActionDescription),
    Priority = -10)]
public class SampleContextAction : ContextActionBase
{
    private readonly ICSharpContextActionDataProvider _provider;

    public SampleContextAction(ICSharpContextActionDataProvider provider)
    {
        _provider = provider;
    }

    public override string Text => Resources.SampleContextActionText;

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
            var commentToken = elementFactory.CreateComment($"// ReSharper SDK: {nameof(SampleContextAction)}");
            commentToken = ModificationUtil.AddChildBefore(declaration, commentToken);

            return x => x.Caret.MoveTo(commentToken.GetDocumentEndOffset(), CaretVisualPlacement.DontScrollIfVisible);
        }
    }
}
