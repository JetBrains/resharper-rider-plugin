using System.Collections.Generic;
using JetBrains.Application.InlayHints;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.Application.UI.Controls.Utils;
using JetBrains.Application.UI.PopupLayout;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.UI.Icons;
using JetBrains.UI.RichText;
using JetBrains.Util;

namespace ReSharperPlugin.CodeVision;

public class SampleAdornmentDataModel : IIntraTextAdornmentDataModel
{
    public SampleAdornmentDataModel(string parameterName)
    {
        Text = parameterName + ":";
    }

    public void ExecuteNavigation(PopupWindowContextSource popupWindowContextSource)
    {
        MessageBox.ShowInfo($"{nameof(SampleAdornmentDataModel)}.{nameof(ExecuteNavigation)}", "ReSharper SDK");
    }

    public RichText Text { get; }
    public bool HasContextMenu { get; }
    public IPresentableItem ContextMenuTitle { get; }
    public IEnumerable<BulbMenuItem> ContextMenuItems { get; }
    public bool IsNavigable { get; }
    public TextRange? SelectionRange { get; }
    public IconId IconId { get; }
    public bool IsPreceding { get; }
    public int Order { get; }
    public InlayHintsMode InlayHintsMode => InlayHintsMode.Always;
}
