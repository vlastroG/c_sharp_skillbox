using PhoneBook.Desktop.ViewModels;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowViewModel loginWindowViewModel)
        {
            DataContext = loginWindowViewModel;
            InitializeComponent();
        }
    }
}
