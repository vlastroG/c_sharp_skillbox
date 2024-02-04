using GoodsStore.Views;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class MainWindowViewModel : BaseViewModel {
        private readonly SqlConnection _clientsDbConnection;
        private readonly SqlDataAdapter _clientsDbDataAdapter;

        private readonly SqlConnection _productsDbConnection;
        private readonly SqlDataAdapter _productsDbDataAdapter;


        internal MainWindowViewModel() {
            var clientsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ClientsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true
            };
            _clientsDbConnection = new SqlConnection(clientsConnection.ConnectionString);
            ClientsDataTable = new DataTable();
            _clientsDbDataAdapter = new SqlDataAdapter();

            var productsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ProductsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true
            };
            _productsDbConnection = new SqlConnection(productsConnection.ConnectionString);
            ProductsDataTable = new DataTable();
            _productsDbDataAdapter = new SqlDataAdapter();

            ConfigClientsDataAdapter();
            ConfigProductsDataAdapter();

            SaveDataCommand = new LambdaCommand(SaveData, CanSaveData);
            DeleteAllDataCommand = new LambdaCommand(DeleteAllData, CanDeleteAllData);
            AddClientCommand = new LambdaCommand(AddClient, CanAddClient);
            DeleteClientCommand = new LambdaCommand(DeleteClient, CanDeleteClient);
            AddProductCommand = new LambdaCommand(AddProduct, CanAddProduct);
            DeleteProductCommand = new LambdaCommand(DeleteProduct, CanDeleteProduct);
        }


        public ICommand SaveDataCommand { get; }

        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

        public ICommand AddProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public ICommand DeleteAllDataCommand { get; }


        internal DataTable ClientsDataTable { get; }
        internal DataRow? SelectedClientsDataRow { get; set; }

        internal DataTable ProductsDataTable { get; }
        internal DataRow? SelectedProductsDataRow { get; set; }


        private void SaveData(object parameter) {
            _clientsDbDataAdapter.Update(ClientsDataTable);
            _productsDbDataAdapter.Update(ProductsDataTable);
        }

        private bool CanSaveData(object parameter) => true;


        private void DeleteAllData(object parameter) {
            ClientsDataTable.Clear();
            _clientsDbDataAdapter.Update(ClientsDataTable);
            ProductsDataTable.Clear();
            _productsDbDataAdapter.Update(ProductsDataTable);
        }

        private bool CanDeleteAllData(object parameter) => true;


        private void AddClient(object parameter) {
            ClientViewModel clientViewModel = new ClientViewModel();
            ClientCreationWindow window = new ClientCreationWindow() { DataContext = clientViewModel };
            window.ShowDialog();
            if (window.DialogResult == true) {
                try {
                    DataRow client = ClientsDataTable.NewRow();
                    client["Surname"] = clientViewModel.Surname;
                    client["Name"] = clientViewModel.Name;
                    client["Patronymic"] = clientViewModel.Patronymic;
                    client["Phone"] = clientViewModel.Phone;
                    client["Email"] = clientViewModel.Email;
                    ClientsDataTable.Rows.Add(client);
                    _clientsDbDataAdapter.Update(ClientsDataTable);
                } catch (Exception e) when (
                       e is ArgumentNullException
                    || e is ArgumentException
                    || e is ConstraintException
                    || e is NoNullAllowedException
                    || e is InvalidOperationException
                    || e is DBConcurrencyException) {
                    MessageBox.Show(e.Message);
                } catch (SystemException sysEx) {
                    MessageBox.Show(sysEx.Message);
                    throw;
                }
            }
        }

        private bool CanAddClient(object parameter) => true;


        private void DeleteClient(object p) {

        }

        private bool CanDeleteClient(object p) => SelectedClientsDataRow != null;


        private void AddProduct(object parameter) {

        }

        private bool CanAddProduct(object parameter) => SelectedClientsDataRow != null;


        private void DeleteProduct(object parameter) {

        }

        private bool CanDeleteProduct(object parameter) => SelectedProductsDataRow != null;


        private void ConfigClientsDataAdapter() {
            string sql = @"SELECT * FROM Clients Order By Clients.Id;";
            _clientsDbDataAdapter.SelectCommand = new SqlCommand(sql, _clientsDbConnection);

            sql = @"INSERT INTO Clients (Surname, Name, Patronymic, Phone, Email) 
                                 VALUES (@Surname, @Name, @Patronymic, @Phone, @Email);";
            _clientsDbDataAdapter.InsertCommand = new SqlCommand(sql, _clientsDbConnection);
            _clientsDbDataAdapter.InsertCommand.Parameters.AddRange(new[] {
               new SqlParameter("@Surname", SqlDbType.NVarChar, 50, "Surname"),
               new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"),
               new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50, "Patronymic"),
               new SqlParameter("@Phone", SqlDbType.NVarChar, 50, "Phone"),
               new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"),
            });

            sql = @"UPDATE Clients SET
                                   Surname = @Surname,
                                   Name = @Name,
                                   Patronymic = @Patronymic,
                                   Phone = @Phone,
                                   Email = @Email
                    WHERE Id = @Id;";
            _clientsDbDataAdapter.UpdateCommand = new SqlCommand(sql, _clientsDbConnection);
            _clientsDbDataAdapter.UpdateCommand.Parameters.AddRange(new[] {
                new SqlParameter("@Id", SqlDbType.Int, 4, "Id"){SourceVersion = DataRowVersion.Original},
                new SqlParameter("@Surname", SqlDbType.NVarChar, 50, "Surname"),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"),
                new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50, "Patronymic"),
                new SqlParameter("@Phone", SqlDbType.NVarChar, 50, "Phone"),
                new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"),
            });

            sql = @"DELETE FROM Clients WHERE Id = @Id;";
            _clientsDbDataAdapter.DeleteCommand = new SqlCommand(sql, _clientsDbConnection);
            _clientsDbDataAdapter.DeleteCommand.Parameters.AddRange(new[] {
                new SqlParameter("Id", SqlDbType.Int, 4, "Id")
            });

            _clientsDbDataAdapter.Fill(ClientsDataTable);
        }

        private void ConfigProductsDataAdapter() {
            string sql = @"SELECT * FROM Products Order By Products.Id;";
            _productsDbDataAdapter.SelectCommand = new SqlCommand(sql, _productsDbConnection);

            sql = @"INSERT INTO Products (Email, ProductCode, Name) 
                                 VALUES (@Email, @ProductCode, @Name);";
            _productsDbDataAdapter.InsertCommand = new SqlCommand(sql, _productsDbConnection);
            _productsDbDataAdapter.InsertCommand.Parameters.AddRange(new[] {
                new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"),
                new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50, "ProductCode"),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"),
            });

            sql = @"UPDATE Products SET
                                   Email = @Email,
                                   ProductCode = @ProductCode,
                                   Name = @Name,
                    WHERE Id = @Id;";
            _productsDbDataAdapter.UpdateCommand = new SqlCommand(sql, _productsDbConnection);
            _productsDbDataAdapter.UpdateCommand.Parameters.AddRange(new[] {
                new SqlParameter("@Id", SqlDbType.Int, 4, "Id"){SourceVersion = DataRowVersion.Original},
                new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"),
                new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50, "ProductCode"),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"),
            });

            sql = @"DELETE FROM Products WHERE Id = @Id;";
            _productsDbDataAdapter.DeleteCommand = new SqlCommand(sql, _productsDbConnection);
            _productsDbDataAdapter.DeleteCommand.Parameters.AddRange(new[] {
                new SqlParameter("Id", SqlDbType.Int, 4, "Id")
            });
        }
    }
}
