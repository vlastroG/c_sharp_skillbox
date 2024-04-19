using PhoneBook.Desktop.ViewModels;
using System.Windows;

namespace PhoneBook.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}