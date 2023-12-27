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


        public virtual string GetClientSurname(Client client) { return client.GetSurname(_login, _password); }

        public virtual string GetClientName(Client client) { return client.GetName(_login, _password); }

        public virtual string GetClientPatronymic(Client client) { return client.GetPatronymic(_login, _password); }

        public virtual string GetClientPassport(Client client) { return client.GetPassport(_login, _password); }

        public virtual string GetClientPhone(Client client) { return client.GetPhone(_login, _password); }

        public virtual Client SetClientPhone(Client client, string phone) {
            client.SetPhone(_login, _password, phone);
            return client;
        }
    }
}
