using BankSystem.Context;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories {
    internal abstract class Repository<TEntity> where TEntity : Entity {
        private readonly ClientsDbContext _clientsDbContext;

        private readonly DbSet<TEntity> _dbSet;


        protected Repository(ClientsDbContext context) {
            _clientsDbContext = context;
            _dbSet = _clientsDbContext.Set<TEntity>();
        }


        public virtual IQueryable<TEntity> Items => _dbSet;


        public TEntity Add(TEntity entity) {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }
            _clientsDbContext.Entry(entity).State = EntityState.Added;
            _clientsDbContext.SaveChanges();
            return entity;
        }

        public void Update(TEntity entity) {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }
            _clientsDbContext.Entry(entity).State = EntityState.Modified;
            _clientsDbContext.SaveChanges();
        }

        public void Remove(int id) {
            var item = _dbSet.FirstOrDefault(i => i.Id == id);
            if (item != null) {
                _clientsDbContext.Remove(item);
                _clientsDbContext.SaveChanges();
            }
        }
    }
}
