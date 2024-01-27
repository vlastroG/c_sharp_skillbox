using Microsoft.Extensions.Configuration;

namespace BankSystem.Entities {
    internal class Manager : Consultant, IManager {
        private readonly string _login;
        private readonly string _password;


        public Manager() : base() {
            var config = new ConfigurationBuilder().AddUserSecrets<Client>().Build();
            _login = config.GetSection("Manager")["Login"]!;
            _password = config.GetSection("Manager")["Password"]!;
        }


        public override string GetClientName(Client client) {
            return client.GetName(_login, _password);
        }

        public Client SetClientName(Client client, string name) {
            OnNotify($"{GetType()} меняет имя клиенту {client.Id} на {name}");
            client.SetName(_login, _password, name);
            return client;
        }

        public override string GetClientSurname(Client client) {
            return client.GetSurname(_login, _password);
        }

        public Client SetClientSurname(Client client, string surname) {
            OnNotify($"{GetType()} меняет фамилию клиенту {client.Id} на {surname}");
            client.SetSurname(_login, _password, surname);
            return client;
        }

        public override string GetClientPatronymic(Client client) {
            return client.GetPatronymic(_login, _password);
        }

        public Client SetClientPatronymic(Client client, string patronymic) {
            OnNotify($"{GetType()} меняет отчество клиенту {client.Id} на {patronymic}");
            client.SetPatronymic(_login, _password, patronymic);
            return client;
        }

        public override string GetClientPhone(Client client) {
            return client.GetPhone(_login, _password);
        }

        public override Client SetClientPhone(Client client, string phone) {
            OnNotify($"{GetType()} меняет телефон клиенту {client.Id} на {phone}");
            client.SetPhone(_login, _password, phone);
            return client;
        }

        public override string GetClientPassport(Client client) {
            return client.GetPassport(_login, _password);
        }

        public Client SetClientPassport(Client client, string passport) {
            OnNotify($"{GetType()} меняет паспорт клиенту {client.Id} на {passport}");
            client.SetPassport(_login, _password, passport);
            return client;
        }

        public Client CreateNewClient(string surname, string name, string patronymic, string passport) {
            return new Client(surname, name, patronymic, passport);
        }
    }
}
