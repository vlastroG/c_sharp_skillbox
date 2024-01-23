using BankSystem.Context;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories {
    internal class BankAccountsGeneralRepository : Repository<BankAccountGeneral> {
        public BankAccountsGeneralRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<BankAccountGeneral> Items => base.Items
            .Where(item => item.GetType() == typeof(BankAccountGeneral))
            .Include(item => item.ClientWithGeneralAccount)
            ;
    }
}
