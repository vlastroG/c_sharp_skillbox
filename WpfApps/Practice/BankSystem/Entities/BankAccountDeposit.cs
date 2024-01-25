namespace BankSystem.Entities {
    /// <summary>
    /// Депозитный счет
    /// </summary>
    internal class BankAccountDeposit : BankAccountGeneral, IBankAccount<BankAccountDeposit> {
        public BankAccountDeposit() : base() {
        }


        public Client? ClientWithDepositAccount { get; set; }


        public int ClientWithDepositAccountId { get; set; }
    }
}
