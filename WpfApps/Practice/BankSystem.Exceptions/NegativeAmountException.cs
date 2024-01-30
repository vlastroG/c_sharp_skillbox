namespace BankSystem.Exceptions {
    /// <summary>
    /// Исключение, если производится операция с отрицательной денежной суммой
    /// </summary>
    public class NegativeAmountException : Exception {
        public NegativeAmountException() : base() { }

        public NegativeAmountException(string message) : base(message) { }
    }
}
