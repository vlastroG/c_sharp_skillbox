﻿using BankSystem.Context;
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

        private protected readonly DepartmentsRepository _departmentsRepository;


        protected ClientsEditorViewModel(ClientsDbContext context) {
            context.Database.Migrate();
            _clientsRepository = new ClientsRepository(context);
            _departmentsRepository = new DepartmentsRepository(context);
            _errorText = string.Empty;
            OkCommand = new LambdaCommand(UpdateData, CanUpdateData);
            Title = "Редактор клиентов";

            AddTestDepartments();
            AddClientsToDepartments();

            Departments = new ObservableCollection<Department>(_departmentsRepository.Items);
            Clients = new ObservableCollection<ClientViewModel>();
        }


        public ObservableCollection<ClientViewModel> Clients { get; }


        public abstract Department? SelectedDepartment { get; set; }

        public ObservableCollection<Department> Departments { get; }


        public string Title { get; private protected set; }


        private string _errorText;
        public string ErrorText {
            get => _errorText;
            set {
                if (Set(ref _errorText, value)) {
                    ClientsHaveNoErrors = string.IsNullOrEmpty(value);
                }
            }
        }


        private bool _clientsHaveNoErrors = true;
        public bool ClientsHaveNoErrors { get => _clientsHaveNoErrors; set => Set(ref _clientsHaveNoErrors, value); }


        public ICommand OkCommand { get; }

        private void UpdateData(object p) {
            foreach (var client in Clients) {
                _clientsRepository.Update(client.GetUpdatedClient());
            }
        }

        private bool CanUpdateData(object p) => (Clients is not null) && ClientsHaveNoErrors;

        private void AddTestDepartments() {
            if (!_departmentsRepository.Items.Any()) {
                var departement1 = new Department("Департамент 1") { Id = 1 };
                var departement2 = new Department("Департамент 2") { Id = 2 };
                var departement3 = new Department("Департамент 3") { Id = 3 };

                _departmentsRepository.Add(departement1);
                _departmentsRepository.Add(departement2);
                _departmentsRepository.Add(departement3);
            }
        }

        private void AddClientsToDepartments() {
            var clients = _clientsRepository.Items.Where(client => client.Department == null);
            var department = _departmentsRepository.Items.First();
            foreach (var client in clients) {
                client.Department = department;
                _clientsRepository.Update(client);
            }
        }
    }
}
