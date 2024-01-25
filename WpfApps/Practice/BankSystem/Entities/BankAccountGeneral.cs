namespace BankSystem.Entities {
    /// <summary>
    /// Недепозитный счет
    /// </summary>
    internal class BankAccountGeneral : Entity, IBankAccount<BankAccountGeneral> {
        public BankAccountGeneral() {
            Number = Guid.NewGuid().ToString();
            IsActive = true;
            Money = 0;
        }


        public string Number { get; }

        public bool IsActive { get; private protected set; }

        public decimal Money { get; private protected set; }

        public Client? ClientWithGeneralAccount { get; set; }

        public int ClientWithGeneralAccountId { get; set; }


        public void Close() {
            IsActive = false;
        }

        public void Open() {
            IsActive = true;
        }

        public void PutMoney(decimal amount) {
            if (amount > 0) {
                Money += amount;
            } else {
                throw new ArgumentException(nameof(amount));
            }
        }

        public decimal GetMoney(decimal amount) {
            if (amount > 0) {
                Money -= amount;
                return amount;
            } else {
                throw new ArgumentException(nameof(amount));
            }
        }
    }
}
