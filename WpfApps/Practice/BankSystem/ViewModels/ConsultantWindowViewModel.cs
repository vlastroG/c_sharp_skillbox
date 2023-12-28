using BankSystem.Context;
using BankSystem.Entities;
using System.ComponentModel;

namespace BankSystem.ViewModels {
    internal class ConsultantWindowViewModel : ClientsEditorViewModel {

        private readonly Consultant _consultant;


        public ConsultantWindowViewModel(ClientsDbContext context) : base(context) {
            _consultant = new Consultant();
            foreach (var client in _clientsRepository.Items) {
                var clientVM = new ClientViewModel(client, _consultant);
                Clients!.Add(clientVM);
                clientVM.PropertyChanged += UpdateErrorText;
            }
            Title = "Пользователь: консультант";
        }


        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
            }
        }
    }
}
