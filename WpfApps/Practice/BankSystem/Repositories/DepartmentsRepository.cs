using BankSystem.Context;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories {
    internal class DepartmentsRepository : Repository<Department> {
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
