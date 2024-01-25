using System.Windows;

namespace BankSystem.Views {
    /// <summary>
    /// Interaction logic for BankAccountEditorWindow.xaml
    /// </summary>
    public partial class BankAccountEditorWindow : Window {
        public BankAccountEditorWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
