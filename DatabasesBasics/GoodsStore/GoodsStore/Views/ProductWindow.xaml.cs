using System.Windows;

namespace GoodsStore.Views {
    public partial class ProductWindow : Window {
        public ProductWindow() {
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
