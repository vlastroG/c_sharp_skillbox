using PhoneBook.Desktop.ViewModels;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowViewModel loginWindowViewModel)
        {
            Owner = Application.Current.MainWindow;
            DataContext = loginWindowViewModel;
            InitializeComponent();
        }
    }
}
