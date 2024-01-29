namespace BankSystem.Notification {
    public delegate void NotificationHandler(DateTime time, string message);

    internal interface INotification {
        public event NotificationHandler? Notify;
    }
}
