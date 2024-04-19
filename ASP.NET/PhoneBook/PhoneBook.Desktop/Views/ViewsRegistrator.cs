using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Desktop.Views
{
    internal static class ViewsRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<MainWindow>()
            .AddTransient<AnonymMainView>()
            .AddTransient<UserMainView>()
            .AddTransient<AdminMainView>()
            .AddTransient<ContactCreationWindow>()
            .AddTransient<ContactEditionWindow>()
            ;
    }
}
