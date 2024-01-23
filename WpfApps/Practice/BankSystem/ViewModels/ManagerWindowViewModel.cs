using BankSystem.Context;
using BankSystem.Entities;
using BankSystem.Views;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;

namespace BankSystem.ViewModels {
    internal class ManagerWindowViewModel : ClientsEditorViewModel {

        private readonly Manager _manager;


        public ManagerWindowViewModel(ClientsDbContext context) : base(context) {
            context.Database.Migrate();
            _manager = new Manager();
            Title = "Пользователь: менеджер";
            CreateClientCommand = new LambdaCommand(CreateClient, CanCreateClient);
            RemoveClientCommand = new LambdaCommand(RemoveClient, CanRemoveClient);
            OpenBankAccountsEditorCommand = new LambdaCommand(OpenBankAccountsEditor, CanOpenBankAccountsEditor);

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
                            var clientVM = new ClientViewModel(client, _manager);
                            clientVM.PropertyChanged += UpdateErrorText;
                            Clients.Add(clientVM);
                        }
                    }
                }
            }
        }

        public ICommand CreateClientCommand { get; }

        public ICommand RemoveClientCommand { get; }

        public ICommand OpenBankAccountsEditorCommand { get; }


        private void CreateClient(object p) {
            var newClientVM = new ClientViewModel(_manager);
            var window = new ClientCreationWindow() { DataContext = newClientVM };
            if (window.ShowDialog() ?? false) {
                Client client = newClientVM.GetUpdatedClient();
                client.Department = SelectedDepartment;
                client.BankAccountGeneral = new BankAccountGeneral();
                client.BankAccountDeposit = new BankAccountDeposit();

                _clientsRepository.Add(client);
                _bankAccountsGeneralRepository.Add(client.BankAccountGeneral);
                _bankAccountsDepositRepository.Add(client.BankAccountDeposit);
                Clients.Add(newClientVM);
                newClientVM.PropertyChanged += UpdateErrorText;
            }
        }

        private bool CanCreateClient(object p) => SelectedDepartment is not null;


        private void RemoveClient(object p) {
            if (SelectedClient != null) {
                _clientsRepository.Remove(SelectedClient.GetUpdatedClient().Id);
                Clients.Remove(SelectedClient);
                UpdateErrorText();
            }
        }

        private bool CanRemoveClient(object p) => SelectedClient != null;


        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                UpdateErrorText();
            }
        }

        private void UpdateErrorText() {
            ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
        }


        private void OpenBankAccountsEditor(object o) {
            var vm = new BankAccountsEditorViewModel(
                _manager,
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
