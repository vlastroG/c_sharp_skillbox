using GoodsStore.Exceptions;
using GoodsStore.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace GoodsStore.Repository {
    internal class ClientsRepository {
        private readonly SqlConnection _clientsDbConnection;
        private readonly SqlDataAdapter _clientsDbDataAdapter;
        private readonly DataTable _clientsDataTable;


        public ClientsRepository() {
            var clientsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ClientsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true
            };
            _clientsDbConnection = new SqlConnection(clientsConnection.ConnectionString);
            _clientsDataTable = new DataTable();
            _clientsDbDataAdapter = new SqlDataAdapter();

            ConfigClientsDataAdapter();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Add(Client client) {
            if (client is null) { throw new ArgumentNullException(nameof(client)); }
            if (string.IsNullOrWhiteSpace(client.Surname)
                || string.IsNullOrWhiteSpace(client.Name)
                || string.IsNullOrWhiteSpace(client.Patronymic)
                || string.IsNullOrWhiteSpace(client.Email)) {
                throw new ArgumentException(nameof(client));
            }

            try {
                DataRow clientRow = _clientsDataTable.NewRow();
                clientRow["Surname"] = client.Surname;
                clientRow["Name"] = client.Name;
                clientRow["Patronymic"] = client.Patronymic;
                clientRow["Phone"] = client.Phone;
                clientRow["Email"] = client.Email;
                clientRow.EndEdit();
                _clientsDataTable.Rows.Add(clientRow);
                _clientsDbDataAdapter.Update(_clientsDataTable);
                client.Id = (int)clientRow["Id"];
            } catch (Exception e) when (
                   e is ArgumentNullException
                || e is ArgumentException
                || e is ConstraintException
                || e is NoNullAllowedException
                || e is InvalidOperationException
                || e is DBConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Client> GetAll() {
            List<Client> list = new List<Client>();
            foreach (DataRow row in _clientsDataTable.Rows) {
                list.Add(new Client() {
                    Id = (int)row["Id"],
                    Name = (string)row["Name"],
                    Surname = (string)row["Surname"],
                    Patronymic = (string)row["Patronymic"],
                    Phone = row["Phone"]?.ToString(),
                    Email = (string)row["Email"]
                });
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Update(Client client) {
            if (client is null) { throw new ArgumentNullException(nameof(client)); }
            if (string.IsNullOrWhiteSpace(client.Surname)
                || string.IsNullOrWhiteSpace(client.Name)
                || string.IsNullOrWhiteSpace(client.Patronymic)
                || string.IsNullOrWhiteSpace(client.Email)) {
                throw new ArgumentException(nameof(client));
            }

            try {
                DataRow? clientRow = _clientsDataTable.Rows.Find(client.Email);
                if (clientRow is null) {
                    throw new RepositoryException($"Клиент с Email {client.Email} отсутствует в репозитории");
                }
                clientRow.BeginEdit();
                clientRow["Surname"] = client.Surname;
                clientRow["Name"] = client.Name;
                clientRow["Patronymic"] = client.Patronymic;
                clientRow["Phone"] = client.Phone;
                clientRow.EndEdit();
                _clientsDbDataAdapter.Update(_clientsDataTable);
            } catch (Exception e) when (
                   e is ArgumentNullException
                || e is ArgumentException
                || e is ConstraintException
                || e is NoNullAllowedException
                || e is InvalidOperationException
                || e is DBConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Delete(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException(nameof(email)); }

            try {
                DataRow? clientRow = _clientsDataTable.Rows.Find(email);
                if (clientRow is null) {
                    throw new RepositoryException($"Клиент с Email {email} отсутствует в репозитории");
                }
                clientRow.Delete();
                _clientsDbDataAdapter.Update(_clientsDataTable);
            } catch (Exception e) when (
                   e is ArgumentNullException
                || e is ArgumentException
                || e is ConstraintException
                || e is NoNullAllowedException
                || e is InvalidOperationException
                || e is DBConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="RepositoryException"></exception>
        public void Clear() {
            try {
                foreach (DataRow row in _clientsDataTable.Rows) {
                    row.Delete();
                }
                _clientsDbDataAdapter.Update(_clientsDataTable);
            } catch (Exception e) when (
                   e is ArgumentNullException
                || e is ArgumentException
                || e is ConstraintException
                || e is NoNullAllowedException
                || e is InvalidOperationException
                || e is DBConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }


        private void ConfigClientsDataAdapter() {
            string sql = @"SELECT * FROM Clients Order By Clients.Id;";
            _clientsDbDataAdapter.SelectCommand = new SqlCommand(sql, _clientsDbConnection);

            sql = @"INSERT INTO Clients (Surname, Name, Patronymic, Phone, Email) 
                                 VALUES (@Surname, @Name, @Patronymic, @Phone, @Email);
                    SET @Id = @@IDENTITY;";
            _clientsDbDataAdapter.InsertCommand = new SqlCommand(sql, _clientsDbConnection);
            _clientsDbDataAdapter.InsertCommand.Parameters.AddRange(new[] {
                new SqlParameter("@Id", SqlDbType.Int, 4, "Id"){ Direction = ParameterDirection.Output },
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

            _clientsDbDataAdapter.Fill(_clientsDataTable);
            _clientsDataTable.PrimaryKey = new DataColumn[] { _clientsDataTable.Columns["Email"]! };
        }
    }
}
