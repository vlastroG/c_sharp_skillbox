using GoodsStore.ViewModels;
using System.Windows;

namespace GoodsStore {
    public partial class MainWindow : Window {
        public MainWindow() {
            var vm = new MainWindowViewModel();
            DataContext = vm;
            InitializeComponent();
        }
    }
}