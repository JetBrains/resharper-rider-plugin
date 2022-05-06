using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Application.DataContext;
using JetBrains.Application.UI.DataContext;
using JetBrains.Application.UI.Options.OptionPages;
using JetBrains.DocumentModel.DataContext;
using JetBrains.Platform.VisualStudio.SinceVs09.Shell.Zones;
using JetBrains.Platform.VisualStudio.SinceVs10.Shell.Zones;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.DataContext;
using JetBrains.ReSharper.UnitTestFramework.Actions;
using JetBrains.TestFramework.Application.Zones;
using JetBrains.TextControl.DataContext;
using JetBrains.VsIntegration.Shell.Zones;

namespace ReSharperPlugin.Miscellaneous
{
    public class ZonesMarker :
        // Required for any Visual Studio components (e.g. DTE)
        IRequire<IVisualStudioFrontendEnvZone>,

        // Required in test assemblies
        IRequire<ITestsEnvZone>,

        IRequire<ICodeEditingZone>,

        IRequire<IToolsOptionsPageImplZone>,

        // Required for when working with PSI
        IRequire<IPsiLanguageZone>
    {
    }

    // NOTE: This plugin is non-functional.
    public class DataContext
    {
        public void M(IDataContext context)
        {
            // PSI
            var declaredElements = context.GetData(PsiDataConstants.DECLARED_ELEMENTS);
            var clrDeclaredElement = context.GetData(PsiDataConstants.TYPE_OR_TYPE_MEMBER);
            var sourceFile = context.GetData(PsiDataConstants.SOURCE_FILE);
            var selectedExpression = context.GetData(PsiDataConstants.SELECTED_EXPRESSION);
            var selectedNodes = context.GetData(PsiDataConstants.SELECTED_TREE_NODES);

            // Document
            var document = context.GetData(DocumentModelDataConstants.DOCUMENT);
            var editorContext = context.GetData(DocumentModelDataConstants.EDITOR_CONTEXT);

            // Project Model
            var project = context.GetData(ProjectModelDataConstants.PROJECT);
            var solution = context.GetData(ProjectModelDataConstants.SOLUTION);

            // Text Control
            var textControl = context.GetData(TextControlDataConstants.TEXT_CONTROL);

            // Unit Testing
            var allElements = context.GetData(UnitTestDataConstants.Elements.ALL);
            var selectedElements = context.GetData(UnitTestDataConstants.Elements.SELECTED);
            var currentSession = context.GetData(UnitTestDataConstants.Session.CURRENT);

            // UI
            var popupWindowContextSource = context.GetData(UIDataConstants.PopupWindowContextSource);
        }
    }
}
