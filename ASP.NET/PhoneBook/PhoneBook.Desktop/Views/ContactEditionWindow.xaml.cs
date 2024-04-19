using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class ContactEditionWindow : Window
    {
        public ContactEditionWindow()
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
        }
    }
}
