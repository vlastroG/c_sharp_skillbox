namespace PhoneBook.Exceptions
{
    /// <summary>
    /// Исключение, когда пользователь не авторизован
    /// </summary>
    public class NotAuthorizedUserException : Exception
    {
        /// <summary>
        /// Исключение, когда пользователь не авторизован
        /// </summary>
        public NotAuthorizedUserException() : base()
        {

        }

        /// <summary>
        /// Исключение, когда пользователь не авторизован
        /// </summary>
        public NotAuthorizedUserException(string msg) : base(msg)
        {

        }
    }
}
