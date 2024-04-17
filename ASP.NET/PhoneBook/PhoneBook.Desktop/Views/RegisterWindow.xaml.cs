using PhoneBook.Desktop.ViewModels;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow(RegisterWindowViewModel registerWindowViewModel)
        {
            Owner = Application.Current.MainWindow;
            DataContext = registerWindowViewModel;
            InitializeComponent();
        }
    }
}
