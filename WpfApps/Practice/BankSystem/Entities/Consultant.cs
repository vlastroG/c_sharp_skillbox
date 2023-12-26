using Microsoft.Extensions.Configuration;

namespace BankSystem.Entities {
    internal class Consultant {
        private readonly string _login;
        private readonly string _password;


        public Consultant() {
            var config = new ConfigurationBuilder().AddUserSecrets<Client>().Build();
            _login = config.GetSection("Consultant")["Login"]!;
            _password = config.GetSection("Consultant")["Password"]!;
        }


        public string GetClientSurname(Client client) { return client.GetSurname(_login, _password); }

        public string GetClientName(Client client) { return client.GetName(_login, _password); }

        public string GetClientPatronymic(Client client) { return client.GetPatronymic(_login, _password); }

        public string GetClientPassport(Client client) { return client.GetPassport(_login, _password); }

        public string GetClientPhone(Client client) { return client.GetPhone(_login, _password); }

        public Client SetClientPhone(Client client, string phone) {
            client.SetPhone(_login, _password, phone);
            return client;
        }
    }
}
