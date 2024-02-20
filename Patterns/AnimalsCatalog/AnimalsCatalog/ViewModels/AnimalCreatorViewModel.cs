using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace AnimalsCatalog.ViewModels {
    public class AnimalCreatorViewModel : BaseViewModel, IDataErrorInfo {
        public AnimalCreatorViewModel() {
            AnimalTypes = new ReadOnlyCollection<string>([
                "Млекопитающие",
                "Птицы",
                "Земноводные",
                "Неизвестно"
            ]);

            SaveCommand = new LambdaCommand(Save, CanSave);
            CancelCommand = new LambdaCommand(Cancel, CanCancel);
        }


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }


        public IReadOnlyCollection<string> AnimalTypes { get; }


        private string _selectedAnimalType = string.Empty;
        public string SelectedAnimalType {
            get => _selectedAnimalType;
            set => Set(ref _selectedAnimalType, value);
        }


        private string _name = string.Empty;
        public string Name {
            get => _name;
            set => Set(ref _name, value);
        }


        public string Error => this[Name];

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case nameof(Name):
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
            if (!CanSave(p)) { return; }

            var window = (Window)p!;
            window.DialogResult = true;
            window.Close();
        }
        private bool CanSave(object p) => string.IsNullOrWhiteSpace(Error) && (p is not null) && (p is Window);


        private void Cancel(object p) {
            if (!CanCancel(p)) { return; }

            var window = (Window)p!;
            window.DialogResult = false;
            window.Close();
        }
        private bool CanCancel(object p) => (p is not null) && (p is Window);
    }
}
