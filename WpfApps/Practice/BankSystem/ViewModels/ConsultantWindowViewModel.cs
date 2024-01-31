using BankSystem.Data.Context;
using BankSystem.Data.Entities;
using BankSystem.Views;
using System.ComponentModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;

namespace BankSystem.ViewModels {
    internal class ConsultantWindowViewModel : ClientsEditorViewModel {

        private readonly Consultant _consultant;


        public ConsultantWindowViewModel(ClientsDbContext context) : base(context) {
            _consultant = new Consultant();
            Title = "Пользователь: консультант";

            SelectedDepartment = Departments.First();
            OpenBankAccountsEditorCommand = new LambdaCommand(OpenBankAccountsEditor, CanOpenBankAccountsEditor);
        }

        public ICommand OpenBankAccountsEditorCommand { get; }


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


        private void OpenBankAccountsEditor(object o) {
            var vm = new BankAccountsEditorViewModel(
                _consultant,
                SelectedClient!.GetUpdatedClient(),
                _clientsRepository,
                _bankAccountsGeneralRepository,
                _bankAccountsDepositRepository);
            var window = new BankAccountEditorWindow() { DataContext = vm };
            window.ShowDialog();
        }

        private bool CanOpenBankAccountsEditor(object o) => SelectedClient is not null;
    }
}
