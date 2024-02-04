using System.Windows;

namespace GoodsStore.Views {
    public partial class ClientCreationWindow : Window {
        public ClientCreationWindow() {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
