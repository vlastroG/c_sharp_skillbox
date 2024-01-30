namespace BankSystem.Data.Entities {
    public interface IBankAccountContravariant<in T> : IBankAccount where T : BankAccountGeneral {
    }
}
