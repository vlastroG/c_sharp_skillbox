using GoodsStore.Context;
using GoodsStore.Exceptions;
using GoodsStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GoodsStore.Repository {
    internal class ProductsRepository {

        private readonly ProductsDbDemoContext _productsDbDemoContext;
        private readonly DbSet<Product> _productsDbDataSet;


        public ProductsRepository() {
            _productsDbDemoContext = new ProductsDbDemoContext();
            _productsDbDataSet = _productsDbDemoContext.Products;
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
                _productsDbDataSet.Add(product);
                _productsDbDemoContext.SaveChanges();
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IQueryable<Product> GetByEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException(nameof(email)); }
            return _productsDbDataSet.Where(x => x.Email == email).OrderBy(e => e.Id);
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
                _productsDbDataSet.Update(product);
                _productsDbDemoContext.SaveChanges();
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

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
                var product = _productsDbDataSet.Find(id);
                if (product is not null) {
                    _productsDbDataSet.Remove(product);
                    _productsDbDemoContext.SaveChanges();
                } else {
                    throw new RepositoryException($"Товар с Id {id} отсутствует в репозитории");
                }
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="RepositoryException"></exception>
        public void Clear() {
            try {
                foreach (var product in _productsDbDataSet) {
                    _productsDbDataSet.Entry(product).State = EntityState.Deleted;
                }
                _productsDbDemoContext.SaveChanges();
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }
    }
}
