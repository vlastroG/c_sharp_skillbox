using GoodsStore.Exceptions;
using GoodsStore.Models;
using GoodsStore.Repository;
using GoodsStore.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class MainWindowViewModel : BaseViewModel {
        private readonly ClientsRepository _clientsRepository;
        private readonly ProductsRepository _productsRepository;


        internal MainWindowViewModel() {
            _clientsRepository = new ClientsRepository();
            _productsRepository = new ProductsRepository();

            Clients = new ObservableCollection<ClientViewModel>(
                _clientsRepository.GetAll().Select(x => new ClientViewModel(x)));
            Products = new ObservableCollection<ProductViewModel>();

            DeleteAllDataCommand = new LambdaCommand(DeleteAllData, CanDeleteAllData);

            AddClientCommand = new LambdaCommand(AddClient, CanAddClient);
            UpdateClientCommand = new LambdaCommand(UpdateClient, CanUpdateClient);
            DeleteClientCommand = new LambdaCommand(DeleteClient, CanDeleteClient);

            AddProductCommand = new LambdaCommand(AddProduct, CanAddProduct);
            UpdateProductCommand = new LambdaCommand(UpdateProduct, CanUpdateProduct);
            DeleteProductCommand = new LambdaCommand(DeleteProduct, CanDeleteProduct);

            SelectedClient = Clients.FirstOrDefault();
        }


        public ICommand AddClientCommand { get; }
        public ICommand UpdateClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public ICommand DeleteAllDataCommand { get; }


        public ObservableCollection<ClientViewModel> Clients { get; }
        private ClientViewModel? _selectedClient;
        public ClientViewModel? SelectedClient {
            get => _selectedClient;
            set {
                if (Set(ref _selectedClient, value)) {
                    Products.Clear();
                    if (value is not null) {
                        foreach (var item in _productsRepository.GetByEmail(value.Email)) {
                            Products.Add(new ProductViewModel(item));
                        }
                    }
                }
            }
        }


        public ObservableCollection<ProductViewModel> Products { get; }
        private ProductViewModel? _selectedProduct;
        public ProductViewModel? SelectedProduct {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }


        private void DeleteAllData(object parameter) {
            try {
                _productsRepository.Clear();
                Products.Clear();
                SelectedProduct = null;
            } catch (RepositoryException e) {
                MessageBox.Show(e.Message);
                return;
            }
            try {
                _clientsRepository.Clear();
                Clients.Clear();
                SelectedClient = null;
            } catch (RepositoryException e) {
                MessageBox.Show(e.Message);
                return;
            }
        }
        private bool CanDeleteAllData(object parameter) => true;


        private void AddClient(object parameter) {
            ClientWindowViewModel clientViewModel = new ClientWindowViewModel("Создание нового клиента", true);
            ClientWindow window = new ClientWindow() { DataContext = clientViewModel };
            window.ShowDialog();
            if (window.DialogResult == true) {
                try {
                    Client client = new Client() {
                        Email = clientViewModel.Email,
                        Name = clientViewModel.Name,
                        Surname = clientViewModel.Surname,
                        Patronymic = clientViewModel.Patronymic,
                        Phone = clientViewModel.Phone
                    };
                    _clientsRepository.Add(client);
                    SelectedClient = new ClientViewModel(client);
                    Clients.Add(SelectedClient);
                } catch (RepositoryException e) {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private bool CanAddClient(object parameter) => true;


        private void UpdateClient(object parameter) {
            ClientWindowViewModel clientViewModel = new ClientWindowViewModel("Редактирование клиента", false) {
                Name = SelectedClient!.Name,
                Surname = SelectedClient.Surname,
                Patronymic = SelectedClient.Patronymic,
                Phone = SelectedClient.Phone,
                Email = SelectedClient.Email
            };
            ClientWindow window = new ClientWindow() { DataContext = clientViewModel };
            window.ShowDialog();
            if (window.DialogResult == true) {
                try {
                    SelectedClient.Name = clientViewModel.Name;
                    SelectedClient.Surname = clientViewModel.Surname;
                    SelectedClient.Patronymic = clientViewModel.Patronymic;
                    SelectedClient.Phone = clientViewModel.Phone;

                    _clientsRepository.Update(SelectedClient.GetUpdatedClient());
                } catch (RepositoryException e) {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private bool CanUpdateClient(object parameter) => SelectedClient is not null;


        private void DeleteClient(object p) {
            try {
                SelectedProduct = null;
                foreach (ProductViewModel item in Products) {
                    _productsRepository.Delete(item.GetUpdatedProduct().Id);
                }
                Products.Clear();
            } catch (RepositoryException e) {
                MessageBox.Show(e.Message);
                return;
            }
            try {
                _clientsRepository.Delete(SelectedClient!.Email);
                Clients.Remove(SelectedClient);
                SelectedClient = null;
            } catch (RepositoryException e) {
                MessageBox.Show(e.Message);
                return;
            }
        }
        private bool CanDeleteClient(object p) => SelectedClient is not null;


        private void AddProduct(object parameter) {
            ProductWindowViewModel productViewModel
                = new ProductWindowViewModel("Создание нового товара", SelectedClient!.Email);
            ProductWindow window = new ProductWindow() { DataContext = productViewModel };
            window.ShowDialog();
            if (window.DialogResult == true) {
                try {
                    Product product = new Product() {
                        Email = productViewModel.Email,
                        Name = productViewModel.Name,
                        ProductCode = productViewModel.ProductCode
                    };
                    _productsRepository.Add(product);
                    SelectedProduct = new ProductViewModel(product);
                    Products.Add(SelectedProduct);
                } catch (RepositoryException e) {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private bool CanAddProduct(object parameter) => SelectedClient is not null;


        private void UpdateProduct(object parameter) {
            ProductWindowViewModel productViewModel
                = new ProductWindowViewModel("Редактирование товара", SelectedProduct!.Email) {
                    Name = SelectedProduct.Name,
                    ProductCode = SelectedProduct.ProductCode
                };
            ProductWindow window = new ProductWindow() { DataContext = productViewModel };
            window.ShowDialog();
            if (window.DialogResult == true) {
                try {
                    SelectedProduct.Name = productViewModel.Name;
                    SelectedProduct.ProductCode = productViewModel.ProductCode;

                    _productsRepository.Update(SelectedProduct.GetUpdatedProduct());
                } catch (RepositoryException e) {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private bool CanUpdateProduct(object parameter) => SelectedProduct is not null;


        private void DeleteProduct(object parameter) {
            try {
                _productsRepository.Delete(SelectedProduct!.Id);
                Products.Remove(SelectedProduct);
                SelectedProduct = null;
            } catch (RepositoryException e) {
                MessageBox.Show(e.Message);
                return;
            }
        }
        private bool CanDeleteProduct(object parameter) => SelectedProduct is not null;
    }
}
