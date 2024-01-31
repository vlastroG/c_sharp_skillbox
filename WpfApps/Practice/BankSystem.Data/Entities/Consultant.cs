using BankSystem.Data.Extensions;
using BankSystem.Data.Notification;
using BankSystem.Exceptions;
using BankSystem.Services;
using Microsoft.Extensions.Configuration;

namespace BankSystem.Data.Entities {
    public class Consultant : IConsultant, INotification {
        private readonly string _login;
        private readonly string _password;

        public event NotificationHandler? Notify = NotificationService.ShowNotification;

        public Consultant() {
            var config = new ConfigurationBuilder().AddUserSecrets<Client>().Build();
            _login = config.GetSection("Consultant")["Login"]!;
            _password = config.GetSection("Consultant")["Password"]!;
        }


        public virtual string GetClientSurname(Client client) { return client.GetSurname(_login, _password); }

        public virtual string GetClientName(Client client) { return client.GetName(_login, _password); }

        public virtual string GetClientPatronymic(Client client) { return client.GetPatronymic(_login, _password); }

        public virtual string GetClientPassport(Client client) { return client.GetPassport(_login, _password); }

        public virtual string GetClientPhone(Client client) { return client.GetPhone(_login, _password); }

        public virtual Client SetClientPhone(Client client, string phone) {
            Notify?.Invoke(DateTime.Now, $"{GetType()} меняет клиенту {client.Id} телефон на {phone}");
            client.SetPhone(_login, _password, phone);
            return client;
        }

        public void OpenAccount(IBankAccount account) {
            Notify?.Invoke(DateTime.Now, $"{GetType()} открывает счет {account.Number}");
            account.Open();
        }

        public void CloseAccount(IBankAccount account) {
            Notify?.Invoke(DateTime.Now, $"{GetType()} закрывает счет {account.Number}");
            account.Close();
        }

        public void PutMoney(IBankAccountCovariant<BankAccountGeneral> bankAccount, decimal amount) {
            try {
                Notify?.Invoke(DateTime.Now, $"{GetType()} кладет на счет {bankAccount.Number} сумму {amount}");
                bankAccount.PutMoneyToAccount(amount);
            } catch (NegativeAmountException) {
                Notify?.Invoke(DateTime.Now, "Ошибка перевода - введена отрицательная сумма");
            }
        }

        public void TransferMoney(
            IBankAccountContravariant<BankAccountDeposit> bankAccountSource,
            IBankAccountContravariant<BankAccountDeposit> bankAccountDestination,
            decimal amount) {

            try {
                Notify?.Invoke(DateTime.Now, $"{GetType()} переводит со счета {bankAccountSource.Number} " +
                    $"на счет {bankAccountDestination.Number} сумму {amount}");
                bankAccountDestination.PutMoney(bankAccountSource.GetMoney(amount));
            } catch (NegativeAmountException) {
                Notify?.Invoke(DateTime.Now, "Ошибка перевода - введена отрицательная сумма");
            }
        }


        private protected void OnNotify(string message) {
            Notify?.Invoke(DateTime.Now, message);
        }
    }
}
