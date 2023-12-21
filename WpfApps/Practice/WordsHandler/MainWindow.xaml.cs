using System.Windows;
using WordsHandler.ViewModels;

namespace WordsHandler {
    public partial class MainWindow : Window {
        public MainWindow() {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}