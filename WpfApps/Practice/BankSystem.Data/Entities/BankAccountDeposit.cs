namespace BankSystem.Data.Entities {
    /// <summary>
    /// Депозитный счет
    /// </summary>
    public class BankAccountDeposit : BankAccountGeneral, IBankAccount<BankAccountDeposit> {
        public BankAccountDeposit() : base() {
        }


        public Client? ClientWithDepositAccount { get; set; }


        public int ClientWithDepositAccountId { get; set; }
    }
}
