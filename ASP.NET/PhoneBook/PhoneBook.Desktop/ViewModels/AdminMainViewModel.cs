using PhoneBook.Desktop.Services;

namespace PhoneBook.Desktop.ViewModels
{
    public class AdminMainViewModel : ContactsListViewModel
    {
        public AdminMainViewModel(ContactsRepository contactsRepository, MessageBoxService messageBoxService) : base(contactsRepository, messageBoxService) { }


        /// <summary>
        /// debug constructor
        /// </summary>
        public AdminMainViewModel() : base() { }


    }
}
