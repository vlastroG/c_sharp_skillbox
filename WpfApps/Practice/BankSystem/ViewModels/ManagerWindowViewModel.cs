using BankSystem.Context;
using BankSystem.Entities;
using BankSystem.Views;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;

namespace BankSystem.ViewModels {
    internal class ManagerWindowViewModel : ClientsEditorViewModel {

        private readonly Manager _consultant;


        public ManagerWindowViewModel(ClientsDbContext context) : base(context) {
            context.Database.Migrate();
            _consultant = new Manager();
            foreach (var client in _clientsRepository.Items) {
                var clientVM = new ClientViewModel(client, _consultant);
                Clients!.Add(clientVM);
                clientVM.PropertyChanged += UpdateErrorText;
            }
            Title = "Пользователь: менеджер";
            CreateClientCommand = new LambdaCommand(CreateClient, CanCreateClient);
        }


        public ICommand CreateClientCommand { get; }

        private void CreateClient(object p) {
            var newClientVM = new ClientViewModel(_consultant);
            var window = new ClientCreationWindow() { DataContext = newClientVM };
            if (window.ShowDialog() ?? false) {
                _clientsRepository.Add(newClientVM.GetUpdatedClient());
                Clients.Add(newClientVM);
                newClientVM.PropertyChanged += UpdateErrorText;
            }
        }

        private bool CanCreateClient(object p) => true;


        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
            }
        }
    }
}
