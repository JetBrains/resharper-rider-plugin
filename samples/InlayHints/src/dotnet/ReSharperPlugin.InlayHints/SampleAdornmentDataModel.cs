using System.Collections.Generic;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.Application.UI.Controls.Utils;
using JetBrains.Application.UI.PopupLayout;
using JetBrains.TextControl.DocumentMarkup.IntraTextAdornments;
using JetBrains.UI.Icons;
using JetBrains.UI.RichText;
using JetBrains.Util;

namespace ReSharperPlugin.InlayHints;

public class SampleAdornmentDataModel : IIntraTextAdornmentDataModel
{
    public SampleAdornmentDataModel(string parameterName)
    {
        Text = parameterName + ":";
        Data = new IntraTextAdornmentData(Text, IconId,
            (IsNavigable ? IntraTextAdornmentFlags.IsNavigable : 0)
          | (HasContextMenu ? IntraTextAdornmentFlags.HasContextMenu : 0)
          | (SelectionRange is not null ? IntraTextAdornmentFlags.HasSelectionRange : 0)
          | IntraTextAdornmentFlags.IsNavigable,
            new IntraTextPlacement(Order), InlayHintsMode);
    }

    public void ExecuteNavigation(PopupWindowContextSource popupWindowContextSource)
    {
        MessageBox.ShowInfo($"{nameof(SampleAdornmentDataModel)}.{nameof(ExecuteNavigation)}", "ReSharper SDK");
    }

    public IntraTextAdornmentData Data { get; }

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
