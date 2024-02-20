using AnimalsCatalog.ViewModels;
using System.Windows;

namespace AnimalsCatalog.Views {
    public partial class AnimalCreationWindow : Window {
        public AnimalCreationWindow(AnimalCreatorViewModel animalCreatorViewModel) {
            DataContext = animalCreatorViewModel;
            InitializeComponent();
        }
    }
}
