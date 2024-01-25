using Microsoft.Extensions.Configuration;

namespace BankSystem.Entities {
    internal class Consultant : IConsultant {
        private readonly string _login;
        private readonly string _password;


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
            client.SetPhone(_login, _password, phone);
            return client;
        }

        public void PutMoney(IBankAccountCovariant<BankAccountGeneral> bankAccount, decimal amount) {
            if (amount > 0) {
                bankAccount.PutMoney(amount);
            }
        }

        public void TransferMoney(
            IBankAccountContravariant<BankAccountDeposit> bankAccountSource,
            IBankAccountContravariant<BankAccountDeposit> bankAccountDestination,
            decimal amount) {

            if (amount > 0) {
                bankAccountDestination.PutMoney(bankAccountSource.GetMoney(amount));
            }
        }


        //private void Test() {
        //    IBankAccount<BankAccountGeneral> bankAccount = new BankAccountGeneral();
        //    IBankAccount<BankAccountDeposit> bankAccountD = new BankAccountDeposit();
        //    PutMoney(bankAccount, 1);
        //    PutMoney(bankAccountD, 1);

        //    TransferMoney(bankAccount, bankAccountD, 100);
        //    TransferMoney(bankAccount, bankAccount, 100);
        //    TransferMoney(bankAccountD, bankAccount, 100);
        //    TransferMoney(bankAccountD, bankAccountD, 100);

        //    var client1 = new Client();
        //    var client2 = new Client();

        //    PutMoney(client1.BankAccountDeposit, 1);
        //    PutMoney(client1.BankAccountGeneral, 1);

        //    TransferMoney(client1.BankAccountGeneral, client2.BankAccountDeposit, 100);
        //    TransferMoney(client1.BankAccountDeposit, client2.BankAccountDeposit, 100);
        //    TransferMoney(client1.BankAccountDeposit, client2.BankAccountGeneral, 100);
        //    TransferMoney(client1.BankAccountGeneral, client2.BankAccountGeneral, 100);
        //}
    }
}
