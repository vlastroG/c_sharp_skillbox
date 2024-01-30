namespace BankSystem.Data.Notification {
    public delegate void NotificationHandler(DateTime time, string message);

    public interface INotification {
        public event NotificationHandler? Notify;
    }
}
