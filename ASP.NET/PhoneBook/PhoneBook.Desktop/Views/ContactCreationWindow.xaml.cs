using PhoneBook.Desktop.ViewModels;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class ContactCreationWindow : Window
    {
        public ContactCreationWindow(ContactCreationWindowViewModel contactCreationWindowViewModel)
        {
            Owner = Application.Current.MainWindow;
            DataContext = contactCreationWindowViewModel;
            InitializeComponent();
        }
    }
}
