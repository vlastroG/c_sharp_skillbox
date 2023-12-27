using BankSystem.Context;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
        }


        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
            }
        }
    }
}
