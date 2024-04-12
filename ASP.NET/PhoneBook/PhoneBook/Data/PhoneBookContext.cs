using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhoneBookContext : IdentityDbContext<ApplicationUser>
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; } = default!;
    }
}
