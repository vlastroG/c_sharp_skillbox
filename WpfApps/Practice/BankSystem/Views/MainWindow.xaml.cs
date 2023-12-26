using BankSystem.Context;
using BankSystem.ViewModels;
using System.Windows;

namespace BankSystem.Views {
    public partial class MainWindow : Window {
        public MainWindow() {
            DataContext = new MainWindowViewModel(new ClientsDbContext());
            InitializeComponent();
        }

        private void ButtonOkClick(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}