using BankSystem.Context;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Repositories {
    internal class BankAccountsDepositRepository : Repository<BankAccountDeposit> {
        public BankAccountsDepositRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<BankAccountDeposit> Items => base.Items
            .Include(item => item.ClientWithDepositAccountId);
    }
}
