using AnimalsCatalog.Services;
using AnimalsCatalog.ViewModels;
using AnimalsCatalog.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AnimalsCatalog {
    public partial class App : System.Windows.Application {
        private readonly IHost _host;


        public App() {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddServices();
                    services.AddViewModels();
                    services.AddViews();
                })
                .Build();
        }


        private async void Application_Startup(object sender, StartupEventArgs e) {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e) {
            using (_host) {
                await _host.StopAsync();
            }
        }
    }
}
