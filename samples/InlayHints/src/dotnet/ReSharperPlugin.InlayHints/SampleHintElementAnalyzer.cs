using System.Linq;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeVision;

[ElementProblemAnalyzer(
    typeof(IAttribute),
    HighlightingTypes = new[] { typeof(SampleInlayHint) })]
public class SampleHintElementAnalyzer : ElementProblemAnalyzer<IAttribute>
{
    private readonly ClrTypeName _theoryAttributeName = new("Xunit.TheoryAttribute");
    private readonly ClrTypeName _inlineDataAttributeName = new("Xunit.InlineDataAttribute");

    protected override void Run(
        IAttribute element,
        ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
        var attributeElement = element.TypeReference?.Resolve().DeclaredElement as ITypeElement;
        if (!Equals(attributeElement?.GetClrName(), _theoryAttributeName))
            return;

        var declaration = CSharpFunctionDeclarationNavigator.GetByAttribute(element);
        var method = (IMethod)declaration?.DeclaredElement;
        if (method == null)
            return;

        var parameterNames = method.Parameters.Select(x => x.ShortName).ToList();
        var attributes = declaration.Attributes.Where(x => x.Name.ShortName == "InlineData");
        // var attributes = method.GetAttributeInstances(_inlineDataAttributeName, inherit: false);
        foreach (var attribute in attributes)
        {
            var parameters = attribute.ConstructorArgumentExpressions;
            // var parameters = attribute.PositionParameters().ToList();
            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                consumer.AddHighlighting(new SampleInlayHint(parameterNames[i], parameter, parameter.GetDocumentStartOffset()));
            }
        }
    }
}
