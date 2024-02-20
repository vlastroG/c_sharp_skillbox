using AnimalsCatalog.Models;
using AnimalsCatalog.Models.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalsCatalog.Services {
    internal static class ServicesRegistrator {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<IAnimalTypesFactory, AnimalTypesFactory>()
            .AddSingleton<IAnimalsFactory, AnimalsFactory>()
            .AddSingleton<IAnimalsSerializationService, AnimalsSerializationService>()
            .AddSingleton<IAnimalsSerializerProvider, AnimalsSerializerProvider>()
            .AddSingleton<IWindowsProvider, WindowsProvider>()
            ;
    }
}
