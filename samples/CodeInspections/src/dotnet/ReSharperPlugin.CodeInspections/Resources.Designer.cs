namespace ReSharperPlugin.CodeInspections
{
  using System;
  using JetBrains.Application.I18n;
  using JetBrains.DataFlow;
  using JetBrains.Diagnostics;
  using JetBrains.Lifetimes;
  using JetBrains.Util;
  using JetBrains.Util.Logging;
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public static class Resources
  {
    private static readonly ILogger ourLog = Logger.GetLogger("ReSharperPlugin.CodeInspections.Resources");

    static Resources()
    {
      CultureContextComponent.Instance.WhenNotNull(Lifetime.Eternal, (lifetime, instance) =>
      {
        lifetime.Bracket(() =>
          {
            ourResourceManager = new Lazy<JetResourceManager>(
              () =>
              {
                return instance
                  .CreateResourceManager("ReSharperPlugin.CodeInspections.Resources", typeof(Resources).Assembly);
              });
          },
          () =>
          {
            ourResourceManager = null;
          });
      });
    }
    
    private static Lazy<JetResourceManager> ourResourceManager = null;
    
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    public static JetResourceManager ResourceManager
    {
      get
      {
        var resourceManager = ourResourceManager;
        if (resourceManager == null)
        {
          return ErrorJetResourceManager.Instance;
        }
        return resourceManager.Value;
      }
    }

    public static string SampleHighlightingTitle => ResourceManager.GetString("SampleHighlightingTitle");
    public static string SampleHighlightingDescription => ResourceManager.GetString("SampleHighlightingDescription");
    public static string SampleHighlightingToolTipFormat => ResourceManager.GetString("SampleHighlightingToolTipFormat");
    public static string SampleHighlightingToolTip => ResourceManager.GetString("SampleHighlightingToolTip");
    public static string SampleHighlightingErrorStripeToolTip => ResourceManager.GetString("SampleHighlightingErrorStripeToolTip");
    public static string SampleQuickFixText => ResourceManager.GetString("SampleQuickFixText");
  }
}