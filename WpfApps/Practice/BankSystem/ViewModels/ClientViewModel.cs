using BankSystem.Entities;
using System.ComponentModel;
using vlastroG.WPF.ViewModels;

namespace BankSystem.ViewModels {
    internal class ClientViewModel : BaseViewModel, IDataErrorInfo {
        private readonly Client _client;
        private readonly Consultant _consultant;


        public ClientViewModel(Manager manager) {
            _client = new Client();
            _consultant = manager;

            Surname = string.Empty;
            Name = string.Empty;
            Patronymic = string.Empty;
            Phone = string.Empty;
            Passport = string.Empty;
        }

        public ClientViewModel(Client client, Consultant consultant) {
            _client = client;
            _consultant = consultant;

            _surname = _consultant.GetClientSurname(_client);
            _name = _consultant.GetClientName(_client);
            _patronymic = _consultant.GetClientPatronymic(_client);
            _phone = _consultant.GetClientPhone(_client);
            _passport = _consultant.GetClientPassport(_client);
        }


        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case nameof(Name):
                    case nameof(Patronymic):
                    case nameof(Passport):
                    case nameof(Phone):
                    case nameof(Surname):
                        string? propertyValue = GetType()
                            .GetProperties()
                            .FirstOrDefault(prop => prop.Name == columnName)
                            ?.GetValue(this)
                            ?.ToString();
                        if (string.IsNullOrWhiteSpace(propertyValue)) {
                            error = "Поле не может быть пустым";
                        } else if (propertyValue.StartsWith(' ') || propertyValue.EndsWith(' ')) {
                            error = "Поле не может начинаться или заканчиваться пробелом";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error {
            get {
                return GetType()
                    .GetProperties()
                    .Select(prop => this[prop.Name])
                    .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error)) ?? string.Empty;
            }
        }

        private string _surname = string.Empty;
        public string Surname {
            get => _surname;
            set {
                if (Set(ref _surname, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _name = string.Empty;
        public string Name {
            get => _name;
            set {
                if (Set(ref _name, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _patronymic = string.Empty;
        public string Patronymic {
            get => _patronymic;
            set {
                if (Set(ref _patronymic, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _phone = string.Empty;
        public string Phone {
            get => _phone;
            set {
                if (Set(ref _phone, value)) {

                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _passport = string.Empty;
        public string Passport {
            get => _passport;
            set {
                if (Set(ref _passport, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }



        public Client GetUpdatedClient() {
            if (_consultant is Manager manager) {
                if (manager.GetClientName(_client) != Name) {
                    manager.SetClientName(_client, Name);
                }
                if (manager.GetClientSurname(_client) != Surname) {
                    manager.SetClientSurname(_client, Surname);
                }
                if (manager.GetClientPatronymic(_client) != Patronymic) {
                    manager.SetClientPatronymic(_client, Patronymic);
                }
                if (manager.GetClientPhone(_client) != Phone) {
                    manager.SetClientPhone(_client, Phone);
                }
                if (manager.GetClientPassport(_client) != Passport) {
                    manager.SetClientPassport(_client, Passport);
                }
            } else if (_consultant is Consultant consultant) {
                if (consultant.GetClientPhone(_client) != Phone) {
                    consultant.SetClientPhone(_client, Phone);
                }
            }
            return _client;
        }
    }
}
