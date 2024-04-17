using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Desktop.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<LoginWindowViewModel>()
            .AddTransient<RegisterWindowViewModel>()
            .AddTransient<AnonymMainViewModel>()
            .AddTransient<UserMainViewModel>()
            .AddTransient<AdminMainViewModel>()
            ;
    }
}
