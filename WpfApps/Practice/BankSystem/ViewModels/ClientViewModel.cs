using BankSystem.Entities;
using System.ComponentModel;
using vlastroG.WPF.ViewModels;

namespace BankSystem.ViewModels {
    internal class ClientViewModel : BaseViewModel, IDataErrorInfo {
        private readonly Client _client;
        private readonly Consultant _consultant;

        public ClientViewModel(Client client, Consultant consultant) {
            _client = client;
            _consultant = consultant;
            _error = string.Empty;

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
                Error = error;
                return error;
            }
        }

        private string _error;
        public string Error { get => _error; set => Set(ref _error, value); }

        private string _surname;
        public string Surname { get => _surname; set => Set(ref _surname, value); }

        private string _name;
        public string Name { get => _name; set => Set(ref _name, value); }

        private string _patronymic;
        public string Patronymic { get => _patronymic; set => Set(ref _patronymic, value); }

        private string _phone;
        public string Phone {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private string _passport;
        public string Passport { get => _passport; set => Set(ref _passport, value); }


        public Client GetUpdatedClient() {
            if (_consultant.GetClientPhone(_client) != Phone) {
                _consultant.SetClientPhone(_client, Phone);
            }
            return _client;
        }
    }
}
