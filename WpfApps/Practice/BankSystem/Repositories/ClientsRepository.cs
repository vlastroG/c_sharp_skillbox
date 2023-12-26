using BankSystem.Context;
using BankSystem.Entities;

namespace BankSystem.Repositories {
    internal class ClientsRepository : Repository<Client> {
        public ClientsRepository(ClientsDbContext context) : base(context) {
        }
    }
}
