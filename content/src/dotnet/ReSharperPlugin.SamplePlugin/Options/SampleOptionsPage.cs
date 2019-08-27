using System;
using System.Linq.Expressions;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.Options.ThemedIcons;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.DataFlow;
using JetBrains.IDE.UI.Extensions;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.Rider.Model.UIAutomation;

namespace ReSharperPlugin.SamplePlugin.Options
{
    [OptionsPage(Id, PageTitle, typeof(OptionsThemedIcons.EnvironmentGeneral),
        ParentId = CodeInspectionPage.PID
//        NestingType = OptionPageNestingType.Inline,
//        IsAlignedWithParent = true,
//        Sequence = 0.1d
    )]
    public class SampleOptionsPage : BeSimpleOptionsPage
    {
        private const string Id = nameof(SampleOptionsPage);
        private const string PageTitle = "Sample Options";

        private readonly Lifetime _lifetime;

        public SampleOptionsPage(
            Lifetime lifetime,
            OptionsPageContext optionsPageContext,
            OptionsSettingsSmartContext optionsSettingsSmartContext)
            : base(lifetime, optionsPageContext, optionsSettingsSmartContext)
        {
            _lifetime = lifetime;

            AddHeader("Sample header");
            AddTextBox((SampleSettings x) => x.SampleText, "Description");
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
}