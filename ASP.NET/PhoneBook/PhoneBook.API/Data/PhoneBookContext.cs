using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook.API.Data
{
    public class PhoneBookContext : IdentityDbContext<ApplicationUser>
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
        {
        }

        public DbSet<Models.Contact> Contact { get; set; } = default!;
    }
}
