using JetBrains.Rider.Model;
using Newtonsoft.Json;

namespace ReSharperPlugin.CefToolWindow.ToolWindow;

public interface ICustomEventData { }

public record ShowMessage(string message) : ICustomEventData;

public static class BeCefToolWindowPanelExtensions
{
    public static void ShowMessage(this BeCefToolWindowPanel panel, string message)
    {
        panel.DispatchCustomEvent(new ShowMessage(message));
    }

    private static void DispatchCustomEvent<T>(this BeCefToolWindowPanel panel, T data)
        where T : ICustomEventData
    {
        var type = data.GetType().Name;
        var detail = new { detail = new { type, data } };
        panel.SendMessage.Sync($"new CustomEvent('messageReceivedEvent', {JsonConvert.SerializeObject(detail)})");
    }
}
