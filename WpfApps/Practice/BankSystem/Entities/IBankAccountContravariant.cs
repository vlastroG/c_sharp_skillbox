namespace BankSystem.Entities {
    internal interface IBankAccountContravariant<in T> : IBankAccount where T : BankAccountGeneral {
    }
}
