using BankSystem.Context;
using BankSystem.Entities;
using BankSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace BankSystem.ViewModels {
    internal class MainWindowViewModel : BaseViewModel {
        private readonly ClientsRepository _clientsRepository;

        private readonly Consultant _consultant;


        public MainWindowViewModel(ClientsDbContext context) {
            context.Database.Migrate();
            _clientsRepository = new ClientsRepository(context);
            _consultant = new Consultant();
            _errorText = string.Empty;
            FillTestData();
            Clients = new ObservableCollection<ClientViewModel>(
                _clientsRepository.Items.Select(item => new ClientViewModel(item, _consultant)));
            foreach (var client in Clients) {
                client.PropertyChanged += UpdateErrorText;
            }
            Title = "Пользователь: консультант";
            OkCommand = new LambdaCommand(UpdateData, CanUpdateData);
        }


        public ObservableCollection<ClientViewModel> Clients { get; }


        public string Title { get; }


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

        private void UpdateErrorText(object? sender, PropertyChangedEventArgs e) {
            if (sender is ClientViewModel) {
                ErrorText = Clients.FirstOrDefault(client => !string.IsNullOrWhiteSpace(client.Error))?.Error ?? string.Empty;
            }
        }
    }
}
