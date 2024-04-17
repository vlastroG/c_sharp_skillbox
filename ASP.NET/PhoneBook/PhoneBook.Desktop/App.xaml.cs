using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Desktop.Services;
using PhoneBook.Desktop.ViewModels;
using PhoneBook.Desktop.Views;
using System.Windows;

namespace PhoneBook.Desktop
{
    public partial class App : Application
    {
        private readonly IHost _host;


        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddViews();
                    services.AddViewModels();
                    services.AddServices();
                })
                .Build();
        }


        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
        }
    }

}
