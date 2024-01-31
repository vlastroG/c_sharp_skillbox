namespace BankSystem.Data.Entities {
    public interface IBankAccountCovariant<out T> : IBankAccount where T : BankAccountGeneral {
    }
}
