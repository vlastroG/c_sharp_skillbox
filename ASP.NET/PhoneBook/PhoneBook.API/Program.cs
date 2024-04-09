
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Auth;
using PhoneBook.API.Data;

namespace PhoneBook.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PhoneBookContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("PhoneBookContext")
                ?? throw new InvalidOperationException("Connection string not found")));

            builder.Services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Password.RequiredLength = 8;
                o.SignIn.RequireConfirmedAccount = false;
                o.SignIn.RequireConfirmedEmail = false;
                o.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<PhoneBookContext>();

            builder.Services.CopnfigureAuthentication(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<PhoneBookContext>();
                    db.Database.Migrate();
                }
            }

            app.Run();
        }
    }
}
