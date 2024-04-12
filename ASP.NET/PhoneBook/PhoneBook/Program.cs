using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
namespace PhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PhoneBookContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("PhoneBookContext")
                ?? throw new InvalidOperationException("Connection string 'PhoneBookContext' not found.")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<PhoneBookContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Admin",
                    policyBuilder => policyBuilder.RequireClaim("AdminAccess", "True"));
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Contacts}/{action=Index}/{id?}");
            app.MapRazorPages();

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
