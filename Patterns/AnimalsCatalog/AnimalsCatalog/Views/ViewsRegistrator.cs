using Microsoft.Extensions.DependencyInjection;

namespace AnimalsCatalog.Views {
    internal static class ViewsRegistrator {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<MainWindow>()
            .AddTransient<AnimalCreationWindow>()
            ;
    }
}
