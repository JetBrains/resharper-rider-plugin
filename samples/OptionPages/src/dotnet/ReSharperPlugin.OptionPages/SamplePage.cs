using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.Controls.FileSystem;
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.Application.UI.Options.OptionsDialog.SimpleOptions.ViewModel;
using JetBrains.DataFlow;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.ReSharper.UnitTestFramework.Resources;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;

namespace ReSharperPlugin.OptionPages;

[OptionsPage(PID, PageTitle, typeof(UnitTestingThemedIcons.Session),
    // Discover derived types of AEmptyOptionsPage
    ParentId = CodeInspectionPage.PID)]
// Inline options page into another options page
// [OptionsPage(PID, PageTitle, typeof(OptionsThemedIcons.EnvironmentGeneral),
//     ParentId = CodeInspectionPage.PID,
//     NestingType = OptionPageNestingType.Inline,
//     IsAlignedWithParent = true,
//     Sequence = 0.1d)]
public class SamplePage : BeSimpleOptionsPage
{
    private const string PID = nameof(SamplePage);
    private const string PageTitle = "ReSharper SDK";

    private readonly Lifetime _lifetime;

    public SamplePage(Lifetime lifetime,
        OptionsPageContext optionsPageContext,
        OptionsSettingsSmartContext optionsSettingsSmartContext,
        IconHostBase iconHost,
        ICommonFileDialogs dialogs)
        : base(lifetime, optionsPageContext, optionsSettingsSmartContext)
    {
        _lifetime = lifetime;

        // Add additional search keywords
        AddKeyword("Sample", "Example", "Preferences"); // TODO: only works for ReSharper?

        AddText("This is a sample options page that works likewise in ReSharper and Rider.");
        AddSpacer();
        AddText($"It allows to view and manipulate values in the {nameof(SampleSettings)} class.");
        AddCommentText("Values are saved in a .dotSettings file.");

        AddHeader("Basic Options");

        AddTextBox((SampleSettings x) => x.String, "String value");
        AddIntOption((SampleSettings x) => x.Integer, "Integer value");
        AddBoolOption((SampleSettings x) => x.Boolean, "Boolean value");

        AddHeader("Advanced Options");

        AddRadioOption((SampleSettings x) => x.RadioSelection, "Enum value",
            Enum.GetValues(typeof(SettingsEnum)).Cast<int>()
                .Select(x => (Value: x, Name: Enum.GetName(typeof(SettingsEnum), x)))
                .Select(x => new RadioOptionPoint(x.Value, x.Name)).ToArray());
        AddComboEnum((SampleSettings x) => x.ComboSelection, "Combo enum value", x => x.ToString());

        // var property = new Property<string>(lifetime, $"{nameof(SampleSettings)}:{nameof(SampleSettings.FolderPath)}");
        // optionsSettingsSmartContext.SetBinding(lifetime, (SampleSettings x) => x.FolderPath, property);
        AddFolderChooserOption(
            (SampleSettings x) => x.FolderPath,
            id: nameof(SampleSettings.FolderPath),
            initialValue: FileSystemPath.Empty,
            iconHost,
            dialogs);
    }

    private BeTextBox AddTextBox<TKeyClass>(Expression<Func<TKeyClass, string>> lambdaExpression, string description)
    {
        var property = new Property<string>(description);
        OptionsSettingsSmartContext.SetBinding(_lifetime, lambdaExpression, property);
        var control = property.GetBeTextBox(_lifetime);
        AddControl(control.WithDescription(description, _lifetime));
        return control;
    }
}
