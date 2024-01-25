namespace BankSystem.Entities {
    internal interface IBankAccountCovariant<out T> : IBankAccount where T : BankAccountGeneral {
    }
}
