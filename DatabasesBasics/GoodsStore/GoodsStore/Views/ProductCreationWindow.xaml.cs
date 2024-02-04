using System.Windows;

namespace GoodsStore.Views {
    public partial class ProductCreationWindow : Window {
        public ProductCreationWindow() {
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
