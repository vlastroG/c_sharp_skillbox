namespace PhoneBook.Exceptions
{
    /// <summary>
    /// Исключение, если нет прав для выполнения операции
    /// </summary>
    public class AccessDeniedException : Exception
    {
        /// <summary>
        /// Исключение, если нет прав для выполнения операции
        /// </summary>
        public AccessDeniedException() : base()
        {

        }

        /// <summary>
        /// Исключение, если нет прав для выполнения операции
        /// </summary>
        public AccessDeniedException(string msg) : base(msg) { }
    }
}
