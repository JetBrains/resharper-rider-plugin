using JetBrains.Application.Notifications;
using JetBrains.Application.Parts;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Tasks;
using JetBrains.ReSharper.UnitTestFramework.Resources;
using JetBrains.Util;

namespace ReSharperPlugin.Notifications;

[SolutionComponent(Instantiation.ContainerAsyncAnyThreadSafe)]
internal class SampleNotification
{
    private readonly Lifetime _lifetime;
    private readonly UserNotifications _userNotifications;

    public SampleNotification(
        Lifetime lifetime,
        ISolutionLoadTasksScheduler scheduler,
        UserNotifications userNotifications)
    {
        _lifetime = lifetime;
        _userNotifications = userNotifications;

        scheduler.EnqueueTask(
            new SolutionLoadTask(
                source: typeof(SampleNotification),
                kind: SolutionLoadTaskKinds.AsLateAsPossible,
                action: ShowNotification));
    }

    private void ShowNotification()
    {
        _userNotifications.CreateNotification(
            _lifetime,
            NotificationSeverity.INFO,
            title: "ReSharper SDK",
            body: "This is a sample notification.",
            closeAfterExecution: true,
            executed: new UserNotificationCommand("Executed", () => MessageBox.ShowInfo("Executed", "ReSharper SDK")),
            dismissed: new UserNotificationCommand("Dismissed", () => MessageBox.ShowInfo("Dismissed", "ReSharper SDK")));
    }
}
