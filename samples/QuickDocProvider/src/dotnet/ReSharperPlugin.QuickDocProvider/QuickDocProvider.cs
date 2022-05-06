using System;
using JetBrains.Application.DataContext;
using JetBrains.Application.UI.DataContext;
using JetBrains.Diagnostics;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.QuickDoc;
using JetBrains.ReSharper.Feature.Services.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.DataContext;
using JetBrains.ReSharper.UnitTestFramework.Actions;
using JetBrains.TextControl.DataContext;

namespace ReSharperPlugin.QuickDocProvider
{
    [SolutionComponent]
    public class QuickDocProvider : IQuickDocProvider
    {
        private readonly ISolution _solution;
        private readonly DocumentManager _documentManager;

        public QuickDocProvider(
            ISolution solution,
            DocumentManager documentManager)
        {
            _solution = solution;
            _documentManager = documentManager;
        }

        public bool CanNavigate(IDataContext context)
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


            return true;
        }

        public void Resolve(IDataContext context, Action<IQuickDocPresenter, PsiLanguageType> resolved)
        {
            var document = context.GetData(DocumentModelDataConstants.DOCUMENT).NotNull();
            var projectFile = _documentManager.TryGetProjectFile(document);

            var defaultLanguage = PresentationUtil.GetPresentationLanguageByContainer(projectFile, _solution);
            resolved(presenter, defaultLanguage);
        }
    }

    public class QuickDocPresenter : IQuickDocPresenter
    {
        public string GetId() => nameof(QuickDocPresenter);

        public QuickDocTitleAndText GetHtml(PsiLanguageType presentationLanguage)
        {
            throw new NotImplementedException();
        }

        public IQuickDocPresenter Resolve(string id)
        {
            throw new NotImplementedException();
        }

        public void OpenInEditor(string navigationId = "")
        {
            throw new NotImplementedException();
        }

        public void ReadMore(string navigationId = "")
        {
            throw new NotImplementedException();
        }
    }
}
