using GoodsStore.Context;
using GoodsStore.Exceptions;
using GoodsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.Repository {
    internal class ClientsRepository {

        private readonly ClientsDbDemoContext _clientsDbDemoContext;
        private readonly DbSet<Client> _clientsDbDataSet;


        public ClientsRepository() {
            _clientsDbDemoContext = new ClientsDbDemoContext();
            _clientsDbDataSet = _clientsDbDemoContext.Clients;
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
                _clientsDbDataSet.Add(client);
                _clientsDbDemoContext.SaveChanges();
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Client> GetAll() {
            return _clientsDbDataSet.OrderBy(e => e.Id);
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
                _clientsDbDataSet.Update(client);
                _clientsDbDemoContext.SaveChanges();
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RepositoryException"></exception>
        public void Delete(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException(nameof(email)); }

            try {
                var client = _clientsDbDataSet.Find(email);
                if (client is not null) {
                    _clientsDbDataSet.Remove(client);
                    _clientsDbDemoContext.SaveChanges();
                } else {
                    throw new RepositoryException($"Клиент с Email {email} отсутствует в репозитории");
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
                foreach (var client in _clientsDbDataSet) {
                    _clientsDbDataSet.Entry(client).State = EntityState.Deleted;
                }
                _clientsDbDemoContext.SaveChanges();
            } catch (Exception e) when (
                   e is DbUpdateException
                || e is DbUpdateConcurrencyException) {

                throw new RepositoryException(e.Message);
            }
        }
    }
}
