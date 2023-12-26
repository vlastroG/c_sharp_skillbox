using Microsoft.Extensions.Configuration;

namespace BankSystem.Entities {
    internal class Client : Entity {
        private static readonly IConfigurationRoot _config = new ConfigurationBuilder().AddUserSecrets<Client>().Build();

        private const string _notAccessibleData = "********";

        private string _surname;

        private string _name;

        private string _patronymic;

        private string _phone;

        private string _passport;


        public Client(string surname, string name, string patronymic, string passport) {
            if (string.IsNullOrWhiteSpace(surname)) {
                throw new ArgumentException($"'{nameof(surname)}' cannot be null or whitespace.", nameof(surname));
            }

            if (string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(patronymic)) {
                throw new ArgumentException($"'{nameof(patronymic)}' cannot be null or whitespace.", nameof(patronymic));
            }

            if (string.IsNullOrWhiteSpace(passport)) {
                throw new ArgumentException($"'{nameof(passport)}' cannot be null or whitespace.", nameof(passport));
            }

            _surname = surname;
            _name = name;
            _patronymic = patronymic;
            _passport = passport;
            _phone = string.Empty;
        }

        public Client() : this("default", "default", "default", "default") { }


        public string GetPassport(string login, string password) {
            if (IsConsultant(login, password)) {
                return _notAccessibleData;
            } else {
                return _notAccessibleData;
            }
        }

        public string GetSurname(string login, string password) {
            if (IsConsultant(login, password)) {
                return _surname;
            } else {
                return _notAccessibleData;
            }
        }

        public string GetName(string login, string password) {
            if (IsConsultant(login, password)) {
                return _name;
            } else {
                return _notAccessibleData;
            }
        }

        public string GetPatronymic(string login, string password) {
            if (IsConsultant(login, password)) {
                return _patronymic;
            } else {
                return _notAccessibleData;
            }
        }

        public string GetPhone(string login, string password) {
            if (IsConsultant(login, password)) {
                return _phone;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetPhone(string login, string password, string newPhone) {
            if (IsConsultant(login, password) && !string.IsNullOrWhiteSpace(newPhone)) {
                _phone = newPhone;
            }
        }


        private bool IsConsultant(string login, string password) {
            return login == _config.GetSection("Consultant")["Login"]
                && password == _config.GetSection("Consultant")["Password"];
        }
    }
}
