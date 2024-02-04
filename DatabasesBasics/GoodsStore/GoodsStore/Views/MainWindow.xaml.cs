using GoodsStore.ViewModels;
using System.Windows;

namespace GoodsStore {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void gridClients_CurrentCellChanged(object sender, EventArgs e) {

        }

        private void gridClients_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e) {

        }

        private void gridProducts_CurrentCellChanged(object sender, EventArgs e) {

        }

        private void gridProducts_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e) {

        }
    }
}