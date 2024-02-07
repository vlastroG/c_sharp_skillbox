using System.ComponentModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class ClientWindowViewModel : BaseViewModel, IDataErrorInfo {
        public ClientWindowViewModel(string title, bool canModifyEmail) {
            if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentException(nameof(title)); }

            CanModifyEmail = canModifyEmail;
            Title = title;
            SaveCommand = new LambdaCommand(Save, CanSave);
        }


        public string Title { get; }


        public ICommand SaveCommand { get; }


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

        private string? _phone;
        public string? Phone {
            get => _phone;
            set {
                if (Set(ref _phone, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _email = string.Empty;
        public string Email {
            get => _email;
            set {
                if (Set(ref _email, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }
        public bool CanModifyEmail { get; }

        public string Error {
            get {
                return GetType()
                    .GetProperties()
                    .Select(prop => this[prop.Name])
                    .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error)) ?? string.Empty;
            }
        }

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case nameof(Surname):
                    case nameof(Name):
                    case nameof(Patronymic):
                    case nameof(Email):
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

        private void Save(object p) {
            //close window
        }

        private bool CanSave(object p) => string.IsNullOrWhiteSpace(Error);
    }
}
