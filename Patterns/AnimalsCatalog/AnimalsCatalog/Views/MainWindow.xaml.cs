using AnimalsCatalog.ViewModels;
using System.Windows;

namespace AnimalsCatalog.Views {
    public partial class MainWindow : Window {
        public MainWindow(MainWindowViewModel mainWindowViewModel) {
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}