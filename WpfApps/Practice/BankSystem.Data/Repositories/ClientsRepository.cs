using BankSystem.Data.Context;
using BankSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Repositories {
    public class ClientsRepository : Repository<Client> {
        public ClientsRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<Client> Items => base.Items
            .Include(item => item.Department)
            .Include(item => item.BankAccountGeneral)
            .Include(item => item.BankAccountDeposit)
            ;
    }
}
