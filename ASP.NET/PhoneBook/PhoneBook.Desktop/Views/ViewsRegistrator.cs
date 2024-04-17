using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Desktop.Views
{
    internal static class ViewsRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<MainWindow>()
            .AddTransient<LoginWindow>()
            .AddTransient<RegisterWindow>()
            .AddTransient<AnonymMainView>()
            .AddTransient<UserMainView>()
            .AddTransient<AdminMainView>()
            ;
    }
}
