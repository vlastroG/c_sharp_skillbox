using Microsoft.EntityFrameworkCore;

namespace PhoneBook.API.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
        {
        }

        public DbSet<Models.Contact> Contact { get; set; } = default!;
    }
}
