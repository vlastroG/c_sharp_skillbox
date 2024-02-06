using System.Windows;

namespace GoodsStore.Views {
    public partial class ClientWindow : Window {
        public ClientWindow() {
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
