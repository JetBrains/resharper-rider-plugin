using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.CSharp.PostfixTemplates;
using JetBrains.ReSharper.Feature.Services.CSharp.PostfixTemplates.Behaviors;
using JetBrains.ReSharper.Feature.Services.CSharp.PostfixTemplates.Contexts;
using JetBrains.ReSharper.Feature.Services.PostfixTemplates;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SamplePlugin
{
    [PostfixTemplate("writeLine", "WriteLine an expression", "Console.WriteLine(expr)")]
    public class SamplePostfixTemplate : CSharpPostfixTemplate
    {
        public override PostfixTemplateInfo TryCreateInfo(CSharpPostfixTemplateContext context)
        {
            var withValuesContexts = CSharpPostfixUtils.FindExpressionWithValuesContexts(context);
            return withValuesContexts.Length == 0
                ? null
                : new PostfixTemplateInfo("writeLine", withValuesContexts, availableInPreciseMode: true);
        }

        public override PostfixTemplateBehavior CreateBehavior(PostfixTemplateInfo info)
        {
            return new SamplePostfixBehavior(info);
        }

        private sealed class SamplePostfixBehavior : CSharpStatementPostfixTemplateBehavior<ICSharpStatement>
        {
            public SamplePostfixBehavior([NotNull] PostfixTemplateInfo info)
                : base(info)
            {
            }

            protected override string ExpressionSelectTitle => "Select expression to WriteLine";

            protected override ICSharpStatement CreateStatement(CSharpElementFactory factory, ICSharpExpression expression)
            {
                // Creating an IDeclaredType will allow using-directives to be added automatically
                var type = TypeFactory.CreateTypeByCLRName("System.Console", expression.GetPsiModule());
                return factory.CreateStatement("$0.WriteLine($1);", type, expression);
            }
        }
    }
}
