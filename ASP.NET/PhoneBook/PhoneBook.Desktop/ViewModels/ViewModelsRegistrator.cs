using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Desktop.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<AnonymMainViewModel>()
            .AddTransient<UserMainViewModel>()
            .AddTransient<AdminMainViewModel>()
            .AddTransient<ContactCreationWindowViewModel>()
            ;
    }
}
