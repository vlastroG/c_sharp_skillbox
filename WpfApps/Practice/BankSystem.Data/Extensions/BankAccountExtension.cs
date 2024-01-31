using BankSystem.Data.Entities;

namespace BankSystem.Data.Extensions {
    public static class BankAccountExtension {
        public static void PutMoneyToAccount(this IBankAccount account, decimal amount) {
            if (amount > 0) {
                account.PutMoney(amount);
            }
        }
    }
}
