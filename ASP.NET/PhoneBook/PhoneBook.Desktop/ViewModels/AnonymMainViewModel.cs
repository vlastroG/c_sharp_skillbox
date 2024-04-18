using PhoneBook.Desktop.Services;

namespace PhoneBook.Desktop.ViewModels
{
    public class AnonymMainViewModel : ContactsListViewModel
    {
        public AnonymMainViewModel(
            ContactsRepository contactsRepository,
            MessageBoxService messageBoxService) : base(contactsRepository, messageBoxService)
        {
        }


        /// <summary>
        /// debug constructor
        /// </summary>
        public AnonymMainViewModel() : base() { }
    }
}
