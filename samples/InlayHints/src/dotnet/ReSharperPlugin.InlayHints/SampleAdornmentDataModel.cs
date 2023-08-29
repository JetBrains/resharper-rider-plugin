using System.Collections.Generic;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.Application.UI.Controls.Utils;
using JetBrains.Application.UI.PopupLayout;
using JetBrains.TextControl.DocumentMarkup.Adornments;
using JetBrains.Util;

namespace ReSharperPlugin.InlayHints;

public class SampleAdornmentDataModel : IAdornmentDataModel
{
    public SampleAdornmentDataModel(string parameterName)
    {
        Data = new AdornmentData()
            .WithText(parameterName + ":")
            .WithFlags(AdornmentFlags.IsNavigable)
            .WithMode(PushToHintMode.PushToShowHints);
    }

    public void ExecuteNavigation(PopupWindowContextSource popupWindowContextSource)
    {
        MessageBox.ShowInfo($"{nameof(SampleAdornmentDataModel)}.{nameof(ExecuteNavigation)}", "ReSharper SDK");
    }

    public AdornmentData Data { get; }
    public IPresentableItem ContextMenuTitle { get; }
    public IEnumerable<BulbMenuItem> ContextMenuItems { get; }
    public TextRange? SelectionRange { get; }
}
