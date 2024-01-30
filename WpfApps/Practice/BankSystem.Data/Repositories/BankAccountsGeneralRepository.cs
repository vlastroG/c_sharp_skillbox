using BankSystem.Data.Context;
using BankSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Repositories {
    public class BankAccountsGeneralRepository : Repository<BankAccountGeneral> {
        public BankAccountsGeneralRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<BankAccountGeneral> Items => base.Items
            .Where(item => item.GetType() == typeof(BankAccountGeneral))
            .Include(item => item.ClientWithGeneralAccount)
            ;
    }
}
