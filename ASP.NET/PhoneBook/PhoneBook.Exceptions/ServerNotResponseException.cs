namespace PhoneBook.Exceptions
{
    /// <summary>
    /// Исключение, когда сервис PhoneBook.API не отвечает
    /// </summary>
    public class ServerNotResponseException : Exception
    {
        /// <summary>
        /// Исключение, когда сервис PhoneBook.API не отвечает
        /// </summary>
        public ServerNotResponseException() : base() { }

        /// <summary>
        /// Исключение, когда сервис PhoneBook.API не отвечает
        /// </summary>
        public ServerNotResponseException(string message) : base(message) { }
    }
}
