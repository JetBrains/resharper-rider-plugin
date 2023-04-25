using JetBrains.Application.DataContext;
using JetBrains.Application.Threading;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Features.Internal.Resources;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;

namespace ReSharperPlugin.Actions;

// Action ID = ClassName.TrimEnd("Action")
// This is also shown in Visual Studio Options > Keyboard > Shortcuts
[Action(
    ResourceType: typeof(Resources),
    TextResourceName: nameof(Resources.SampleActionText),
    DescriptionResourceName = nameof(Resources.SampleActionText),
    // Icon must also be changed in frontend code
    Icon = typeof(FeaturesInternalThemedIcons.QuickStartToolWindow))]
public class SampleAction : IExecutableAction
{
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
        return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
        var dialogHost = context.GetComponent<IDialogHost>();

        BeTextBox textBox;

        dialogHost.Show(
            getDialog: lt => BeControls.GetDialog(
                    dialogContent: textBox = BeControls.GetTextBox(lt),
                    title: Resources.SampleActionDialogCaption,
                    id: nameof(SampleAction))
                .WithOkButton(lt, () => MessageBox.ShowInfo($"Okay {textBox.GetText()}"))
                .WithCancelButton(lt),
            parentLifetime: Lifetime.Eternal);
    }
}
