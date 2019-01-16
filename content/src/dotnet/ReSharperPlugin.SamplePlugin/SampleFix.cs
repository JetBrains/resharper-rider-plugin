using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.SamplePlugin
{
    public sealed class SampleFix : QuickFixBase
    {
        private readonly ICSharpDeclaration _declaration;

        public SampleFix(ICSharpDeclaration declaration)
        {
            _declaration = declaration;
        }

        public override string Text => "Write all lower-case";

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return _declaration.IsValid() && _declaration is IMethodDeclaration;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var methodDeclaration = (IMethodDeclaration) _declaration;
            
            // This is greatly simplified, since we're not updating any references
            // You will probably see a small indicator in the lower-right
            // that tells you about an exception being thrown.
            methodDeclaration.SetName(methodDeclaration.DeclaredName.ToLower());
            
            return null;
        }
    }
}