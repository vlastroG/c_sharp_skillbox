using BankSystem.Data.Context;
using BankSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Repositories {
    public class DepartmentsRepository : Repository<Department> {
        public DepartmentsRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<Department> Items => base.Items
            .Include(item => item.Clients)
                .ThenInclude(item => item.BankAccountGeneral)
            .Include(item => item.Clients)
                .ThenInclude(item => item.BankAccountDeposit)
            ;
    }
}
