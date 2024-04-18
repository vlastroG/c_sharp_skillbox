using PhoneBook.Models;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class ContactDetailsWindow : Window
    {
        public ContactDetailsWindow(Contact contact)
        {
            Owner = Application.Current.MainWindow;
            DataContext = contact;
            InitializeComponent();
        }
    }
}
