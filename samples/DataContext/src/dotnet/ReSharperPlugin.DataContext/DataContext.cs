using JetBrains.Application.DataContext;
using JetBrains.Application.UI.DataContext;
using JetBrains.DocumentModel.DataContext;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Psi.DataContext;
using JetBrains.ReSharper.UnitTestFramework.Actions;
using JetBrains.TextControl.DataContext;

namespace ReSharperPlugin.DataContext
{
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
            var allElements = context.GetData(UnitTestDataConstants.TREE.Elements.All);
            var selectedElements = context.GetData(UnitTestDataConstants.TREE.Elements.Selected);
            var currentSession = context.GetData(UnitTestDataConstants.Session.CURRENT);

            // UI
            var popupWindowContextSource = context.GetData(UIDataConstants.PopupWindowContextSource);
        }
    }
}
