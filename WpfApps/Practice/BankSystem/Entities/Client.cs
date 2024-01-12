using Microsoft.Extensions.Configuration;

namespace BankSystem.Entities {
    internal class Client : Entity, IEquatable<Client?> {
        private static readonly IConfigurationRoot _config = new ConfigurationBuilder().AddUserSecrets<Client>().Build();

        private const string _notAccessibleData = "********";

        private string _surname;

        private string _name;

        private string _patronymic;

        private string _phone;

        private string _passport;

        private DateTime _lastChangeTime;

        private string _lastChangeData;

        private string _lastChangeDescription;

        private string _lastChangeBy;


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

            _lastChangeTime = DateTime.Now;
            _lastChangeData = "Создана запись";
            _lastChangeDescription = string.Empty;
            _lastChangeBy = string.Empty;
        }

        public Client() : this("default", "default", "default", "default") { }


        public Department? Department { get; set; }


        public string GetPassport(string login, string password) {
            if (IsConsultant(login, password)) {
                return _notAccessibleData;
            } else if (IsManager(login, password)) {
                return _passport;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetPassport(string login, string password, string passport) {
            if (IsManager(login, password) && !string.IsNullOrWhiteSpace(passport)) {
                _lastChangeTime = DateTime.Now;
                _lastChangeData = nameof(_passport);
                _lastChangeDescription = $"{_passport} => {passport}";
                _lastChangeBy = nameof(Manager);

                _passport = passport;
            }
        }

        public string GetSurname(string login, string password) {
            if (IsConsultant(login, password) || IsManager(login, password)) {
                return _surname;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetSurname(string login, string password, string surname) {
            if (IsManager(login, password) && !string.IsNullOrWhiteSpace(surname)) {
                _lastChangeTime = DateTime.Now;
                _lastChangeData = nameof(_surname);
                _lastChangeDescription = $"{_surname} => {surname}";
                _lastChangeBy = nameof(Manager);

                _surname = surname;
            }
        }

        public string GetName(string login, string password) {
            if (IsConsultant(login, password) || IsManager(login, password)) {
                return _name;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetName(string login, string password, string name) {
            if (IsManager(login, password) && !string.IsNullOrWhiteSpace(name)) {
                _lastChangeTime = DateTime.Now;
                _lastChangeData = nameof(_name);
                _lastChangeDescription = $"{_name} => {name}";
                _lastChangeBy = nameof(Manager);

                _name = name;
            }
        }

        public string GetPatronymic(string login, string password) {
            if (IsConsultant(login, password) || IsManager(login, password)) {
                return _patronymic;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetPatronymic(string login, string password, string patronymic) {
            if (IsManager(login, password) && !string.IsNullOrWhiteSpace(patronymic)) {
                _lastChangeTime = DateTime.Now;
                _lastChangeData = nameof(_patronymic);
                _lastChangeDescription = $"{_patronymic} => {patronymic}";
                _lastChangeBy = nameof(Manager);

                _patronymic = patronymic;
            }
        }

        public string GetPhone(string login, string password) {
            if (IsConsultant(login, password) || IsManager(login, password)) {
                return _phone;
            } else {
                return _notAccessibleData;
            }
        }

        public void SetPhone(string login, string password, string newPhone) {
            if ((IsConsultant(login, password) || IsManager(login, password)) && !string.IsNullOrWhiteSpace(newPhone)) {
                _lastChangeTime = DateTime.Now;
                _lastChangeData = nameof(_phone);
                _lastChangeDescription = $"{_phone} => {newPhone}";

                _lastChangeBy = IsManager(login, password) ? nameof(Manager) : nameof(Consultant);

                _phone = newPhone;
            }
        }
        public override bool Equals(object? obj) {
            return Equals(obj as Client);
        }

        public bool Equals(Client? other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }

            if (ReferenceEquals(this, other)) {
                return true;
            }

            return _surname == other._surname &&
                   _name == other._name &&
                   _patronymic == other._patronymic &&
                   _phone == other._phone &&
                   _passport == other._passport;
        }

        public override int GetHashCode() {
            return HashCode.Combine(_surname, _name, _patronymic, _phone, _passport);
        }


        private bool IsConsultant(string login, string password) {
            return login == _config.GetSection("Consultant")["Login"]
                && password == _config.GetSection("Consultant")["Password"];
        }

        private bool IsManager(string login, string password) {
            return login == _config.GetSection("Manager")["Login"]
                && password == _config.GetSection("Manager")["Password"];
        }
    }
}
