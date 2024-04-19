using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

            builder.Services.AddSingleton<IJwtFactory, JwtFactory>();

            builder.Services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 8;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;

                o.SignIn.RequireConfirmedAccount = false;
                o.SignIn.RequireConfirmedEmail = false;
                o.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<PhoneBookContext>();

            builder.Services.CopnfigureAuthentication(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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
