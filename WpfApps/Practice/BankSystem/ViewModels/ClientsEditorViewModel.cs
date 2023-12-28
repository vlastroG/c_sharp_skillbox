using BankSystem.Context;
using BankSystem.Entities;
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
            FillTestData();
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


        private void FillTestData() {
            if (!_clientsRepository.Items.Any()) {
                _clientsRepository.Add(new Client("Иванов", "Иван", "Иванович", "0000000000"));
                _clientsRepository.Add(new Client("Петров", "Петр", "Петрович", "1111111111"));
                _clientsRepository.Add(new Client("Николаев", "Николай", "Николаевич", "2222222222"));
                _clientsRepository.Add(new Client("Сидоров", "Анатолий", "Анатольевич", "3333333333"));
                _clientsRepository.Add(new Client("Петренко", "Станислав", "Владимирович", "4444444444"));
                _clientsRepository.Add(new Client("Сергеев", "Сергей", "Сергеевич", "5555555555"));
            }
        }
    }
}
