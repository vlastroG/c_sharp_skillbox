using BankSystem.Context;
using BankSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace BankSystem.ViewModels {
    internal abstract class ClientsEditorViewModel : BaseViewModel {
        private protected readonly ClientsRepository _clientsRepository;


        protected ClientsEditorViewModel(ClientsDbContext context) {
            context.Database.Migrate();
            _clientsRepository = new ClientsRepository(context);
            _errorText = string.Empty;
            OkCommand = new LambdaCommand(UpdateData, CanUpdateData);
            Clients = new ObservableCollection<ClientViewModel>();
            Title = "Редактор клиентов";
        }


        public ObservableCollection<ClientViewModel> Clients { get; }


        public string Title { get; private protected set; }


        private string _errorText;
        public string ErrorText { get => _errorText; set => Set(ref _errorText, value); }


        public ICommand OkCommand { get; }

        private void UpdateData(object p) {
            foreach (var client in Clients) {
                _clientsRepository.Update(client.GetUpdatedClient());
            }
        }

        private bool CanUpdateData(object p) => string.IsNullOrWhiteSpace(ErrorText);
    }
}
