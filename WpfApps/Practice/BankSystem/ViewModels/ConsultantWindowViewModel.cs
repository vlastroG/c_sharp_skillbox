using BankSystem.Context;
using BankSystem.Entities;
using System.ComponentModel;

namespace BankSystem.ViewModels {
    internal class ConsultantWindowViewModel : ClientsEditorViewModel {

        private readonly Consultant _consultant;


        public ConsultantWindowViewModel(ClientsDbContext context) : base(context) {
            _consultant = new Consultant();
            Title = "Пользователь: консультант";

            SelectedDepartment = Departments.First();
        }


        private Department? _selectedDepartment;

        public override Department? SelectedDepartment {
            get => _selectedDepartment;
            set {
                if (Set(ref _selectedDepartment, value)) {
                    Clients.Clear();
                    if (_selectedDepartment != null) {
                        foreach (var client in _selectedDepartment.Clients) {
                            var clientVM = new ClientViewModel(client, _consultant);
                            Clients.Add(clientVM);
                            clientVM.PropertyChanged += UpdateErrorText;
                        }
                    }
                }
            }
        }


        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
            }
        }
    }
}
