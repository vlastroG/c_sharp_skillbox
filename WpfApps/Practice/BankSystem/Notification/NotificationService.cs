using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;

namespace BankSystem.Notification {
    internal class NotificationService {
        public static void ShowNotification(DateTime time, string description) {
            WriteLogMessage(time.ToString("yyyy.MM.dd HH:mm:ss") + "-->" + description);

            new ToastContentBuilder()
                .AddText(nameof(BankSystem) + " " + time.ToString("HH:mm:ss"))
                .AddText(description)
                .Show();
        }

        private static void WriteLogMessage(string message) {
            File.AppendAllText("log.txt", message + Environment.NewLine);
        }
    }
}
