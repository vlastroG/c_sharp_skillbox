using BankSystem.Data.Context;
using BankSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Repositories {
    public class BankAccountsDepositRepository : Repository<BankAccountDeposit> {
        public BankAccountsDepositRepository(ClientsDbContext context) : base(context) {
        }


        public override IQueryable<BankAccountDeposit> Items => base.Items
            .Include(item => item.ClientWithDepositAccountId);
    }
}
