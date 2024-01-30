using BankSystem.Data.Notification;
using BankSystem.Exceptions;
using BankSystem.Services;

namespace BankSystem.Data.Entities {
    /// <summary>
    /// Недепозитный счет
    /// </summary>
    public class BankAccountGeneral : Entity, IBankAccount<BankAccountGeneral>, INotification {
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

        public event NotificationHandler? Notify = NotificationService.ShowNotification;

        public void Close() {
            IsActive = false;
            Notify?.Invoke(DateTime.Now, $"Банковский счет {Number} закрыт");
        }

        public void Open() {
            IsActive = true;
            Notify?.Invoke(DateTime.Now, $"Банковский счет {Number} открыт");
        }

        public void PutMoney(decimal amount) {
            if (amount > 0) {
                Money += amount;
                Notify?.Invoke(DateTime.Now, $"Банковский счет {Number} пополнен на {amount}");
            } else {
                throw new NegativeAmountException(amount.ToString());
            }
        }

        public decimal GetMoney(decimal amount) {
            if (amount > 0) {
                Money -= amount;
                Notify?.Invoke(DateTime.Now, $"С банковского счета {Number} снята сумма {amount}");
                return amount;
            } else {
                throw new NegativeAmountException(amount.ToString());
            }
        }
    }
}
