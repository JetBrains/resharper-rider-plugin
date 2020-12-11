using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.ExternalSources.Resources;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.Icons;

namespace ReSharperPlugin.SamplePlugin
{
    [SolutionComponent]
    public class SampleIconProvider : IDeclaredElementIconProvider
    {
        public SampleIconProvider(
            Lifetime lifetime,
            PsiIconManager psiIconManager)
        {
            psiIconManager.AddExtension(lifetime, this);
        }

        public IconId GetImageId(
            IDeclaredElement declaredElement,
            PsiLanguageType languageType,
            out bool canApplyExtensions)
        {
            var typeMember = declaredElement as ITypeMember;
            canApplyExtensions = false;
            return typeMember?.ShortName.StartsWith("Foo") ?? false ? ExternalSourcesThemedIcons.Mask.Id : null;
        }
    }
}
