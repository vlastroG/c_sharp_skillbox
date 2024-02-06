using GoodsStore.Exceptions;
using GoodsStore.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace GoodsStore.Repository {
    internal class ProductsRepository {
        private readonly SqlConnection _productsDbConnection;
        private readonly SqlDataAdapter _productsDbDataAdapter;
        private readonly DataTable _productsDataTable;


        public ProductsRepository() {
            var productsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ProductsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true
            };
            _productsDbConnection = new SqlConnection(productsConnection.ConnectionString);
            _productsDataTable = new DataTable();
            _productsDbDataAdapter = new SqlDataAdapter();

            ConfigProductsDataAdapter();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Add(Product product) {
            if (product is null) { throw new ArgumentNullException(nameof(product)); }
            if (string.IsNullOrWhiteSpace(product.Name)
                || string.IsNullOrWhiteSpace(product.Email)
                || string.IsNullOrWhiteSpace(product.ProductCode)) {
                throw new ArgumentException(nameof(product));
            }

            try {
                DataRow productRow = _productsDataTable.NewRow();
                productRow["Id"] = product.Id;
                productRow["Name"] = product.Name;
                productRow["Email"] = product.Email;
                productRow["ProductCode"] = product.ProductCode;
                productRow.EndEdit();
                _productsDataTable.Rows.Add(productRow);
                _productsDbDataAdapter.Update(_productsDataTable);
                product.Id = (int)productRow["Id"];
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
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Product> GetByEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException(nameof(email)); }

            List<Product> list = new List<Product>();
            foreach (DataRow row in _productsDataTable.Rows) {
                if (row["Email"].ToString() == email) {
                    list.Add(new Product() {
                        Id = (int)row["Id"],
                        Name = (string)row["Name"],
                        ProductCode = (string)row["ProductCode"],
                        Email = (string)row["Email"]
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Update(Product product) {
            if (product is null) { throw new ArgumentNullException(nameof(product)); }
            if (string.IsNullOrWhiteSpace(product.Name)
                || string.IsNullOrWhiteSpace(product.Email)
                || string.IsNullOrWhiteSpace(product.ProductCode)) {
                throw new ArgumentException(nameof(product));
            }

            try {
                DataRow? productRow = _productsDataTable.Rows.Find(product.Id);
                if (productRow is null) {
                    throw new RepositoryException($"Товар с Id {product.Id} отсутствует в репозитории");
                }
                productRow.BeginEdit();
                productRow["Name"] = product.Name;
                productRow["Email"] = product.Email;
                productRow["ProductCode"] = product.ProductCode;
                productRow.EndEdit();
                _productsDbDataAdapter.Update(_productsDataTable);
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
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Delete(int id) {
            if (id < 0) { throw new ArgumentException(nameof(id)); }

            try {
                DataRow? productRow = _productsDataTable.Rows.Find(id);
                if (productRow is null) {
                    throw new RepositoryException($"Товар с Id {id} отсутствует в репозитории");
                }
                productRow.Delete();
                _productsDbDataAdapter.Update(_productsDataTable);
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
                foreach (DataRow row in _productsDataTable.Rows) {
                    row.Delete();
                }
                _productsDbDataAdapter.Update(_productsDataTable);
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


        private void ConfigProductsDataAdapter() {
            string sql = @"SELECT * FROM Products Order By Products.Id;";
            _productsDbDataAdapter.SelectCommand = new SqlCommand(sql, _productsDbConnection);

            sql = @"INSERT INTO Products (Email, ProductCode, Name) 
                                 VALUES (@Email, @ProductCode, @Name);
                    SET @Id = @@IDENTITY;";
            _productsDbDataAdapter.InsertCommand = new SqlCommand(sql, _productsDbConnection);
            _productsDbDataAdapter.InsertCommand.Parameters.AddRange(new[] {
                new SqlParameter("@Id", SqlDbType.Int, 4, "Id"){ Direction = ParameterDirection.Output },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"),
                new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50, "ProductCode"),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"),
            });

            sql = @"UPDATE Products SET
                                   Email = @Email,
                                   ProductCode = @ProductCode,
                                   Name = @Name
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

            _productsDbDataAdapter.Fill(_productsDataTable);
            _productsDataTable.PrimaryKey = new DataColumn[] { _productsDataTable.Columns["Id"]! };
        }
    }
}
