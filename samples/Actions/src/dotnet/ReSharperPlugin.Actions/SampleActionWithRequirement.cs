using JetBrains.Application.DataContext;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.DocumentModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Actions;
using JetBrains.ReSharper.Psi.Files;

namespace ReSharperPlugin.Actions;

public class SampleActionWithRequirement : IActionWithUpdateRequirement
{
    public IActionRequirement GetRequirement(IDataContext dataContext)
    {
        return dataContext.GetData(DocumentModelDataConstants.DOCUMENT) != null
            ? CurrentPsiFileRequirement.FromDataContext(dataContext)
            : CommitAllDocumentsRequirement.TryGetInstance(dataContext);
    }
}
