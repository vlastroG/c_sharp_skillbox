using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Desktop.Views;
using PhoneBook.Exceptions;
using PhoneBook.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public abstract class ContactsListViewModel : BaseViewModel
    {
        private protected readonly ContactsRepository _contactsRepository;
        private protected readonly MessageBoxService _messageBoxService;

        protected ContactsListViewModel(ContactsRepository contactsRepository, MessageBoxService messageBoxService)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            Contacts = new ObservableCollection<Contact>();

            ShowDetailsCommand = new LambdaCommand(ShowDetails, CanShowDetails);
        }

        /// <summary>
        /// debug constructor
        /// </summary>
        protected ContactsListViewModel()
        {
            Contacts = new ObservableCollection<Contact>() { new Contact() };
        }


        public ObservableCollection<Contact> Contacts { get; }

        public ICommand ShowDetailsCommand { get; }


        private bool _commandExecuting = false;

        public bool CommandExecuting
        {
            get => _commandExecuting;
            set => Set(ref _commandExecuting, value);
        }


        public async Task Update()
        {
            Contacts.Clear();
            try
            {
                CommandExecuting = true;
                var contacts = await _contactsRepository.Get();
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError("Сервер не отвечает", "Статус обновления");
            }
            finally
            {
                CommandExecuting = false;
            }
        }


        private void ShowDetails(object? p)
        {
            Contact contact = (p as Contact)!;
            var window = new ContactDetailsWindow(contact);
            window.ShowDialog();
        }

        private bool CanShowDetails(object? p) => p is not null && p is Contact;
    }
}
