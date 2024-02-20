using Microsoft.Extensions.DependencyInjection;

namespace AnimalsCatalog.ViewModels {
    internal static class ViewModelsRegistrator {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<AnimalCreatorViewModel>()
            ;
    }
}
