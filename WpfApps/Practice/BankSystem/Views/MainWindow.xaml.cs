using System.Windows;

namespace BankSystem.Views {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void ManagerClick(object sender, RoutedEventArgs e) {
            Hide();
            var window = new ManagerWindow() { Owner = this };
            window.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void ConsultantClick(object sender, RoutedEventArgs e) {
            Hide();
            var window = new ConsultantWindow() { Owner = this };
            window.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
