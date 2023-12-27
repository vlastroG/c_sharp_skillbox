using BankSystem.Context;
using BankSystem.ViewModels;
using System.Windows;

namespace BankSystem.Views {
    public partial class ManagerWindow : Window {
        public ManagerWindow() {
            DataContext = new ManagerWindowViewModel(new ClientsDbContext());
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
