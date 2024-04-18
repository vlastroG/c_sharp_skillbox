using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Desktop.Views;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class UserMainViewModel : ContactsListViewModel
    {
        private protected readonly IServiceProvider _serviceProvider;

        public UserMainViewModel(ContactsRepository contactsRepository, MessageBoxService messageBoxService, IServiceProvider serviceProvider) : base(contactsRepository, messageBoxService)
        {
            CreateContactCommand = new LambdaCommandAsync(CreateContact, CanCreateContact);
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public ICommand CreateContactCommand { get; }


        private async Task CreateContact(object? p)
        {
            var window = _serviceProvider.GetRequiredService<ContactCreationWindow>();
            window.ShowDialog();
            await Update();
        }

        private bool CanCreateContact(object? p) => true;
    }
}
