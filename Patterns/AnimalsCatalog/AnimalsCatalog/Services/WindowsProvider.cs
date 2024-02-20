using AnimalsCatalog.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalsCatalog.Services {
    public class WindowsProvider : IWindowsProvider {
        private readonly IServiceProvider _serviceProvider;

        public WindowsProvider(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public AnimalCreationWindow AnimalCreationWindow => _serviceProvider.GetRequiredService<AnimalCreationWindow>();
    }
}
