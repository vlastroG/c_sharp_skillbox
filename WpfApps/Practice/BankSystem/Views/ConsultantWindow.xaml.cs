using BankSystem.Data.Context;
using BankSystem.ViewModels;
using System.Windows;

namespace BankSystem.Views {
    public partial class ConsultantWindow : Window {
        public ConsultantWindow() {
            DataContext = new ConsultantWindowViewModel(new ClientsDbContext());
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