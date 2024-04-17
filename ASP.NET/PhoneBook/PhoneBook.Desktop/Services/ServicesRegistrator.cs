using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Desktop.Services
{
    internal static class ServicesRegistrator
    {
        internal static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddHttpClient()
            .AddSingleton<AccountService>()
            .AddSingleton<MessageBoxService>()
            ;
    }
}
