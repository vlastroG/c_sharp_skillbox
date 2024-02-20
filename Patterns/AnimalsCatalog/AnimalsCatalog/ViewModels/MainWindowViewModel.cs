using AnimalsCatalog.Models;
using AnimalsCatalog.Models.Serialization;
using AnimalsCatalog.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace AnimalsCatalog.ViewModels {
    public class MainWindowViewModel : BaseViewModel {
        private readonly IWindowsProvider _windowsProvider;
        private readonly IAnimalsSerializationService _animalsSerializationService;
        private readonly IAnimalsSerializerProvider _animalsSerializerProvider;
        private readonly IAnimalsFactory _animalsFactory;


        public MainWindowViewModel(
            IWindowsProvider windowsProvider,
            IAnimalsSerializationService animalsSerializationService,
            IAnimalsSerializerProvider animalsSerializerProvider,
            IAnimalsFactory animalsFactory) {
            _windowsProvider = windowsProvider
                ?? throw new ArgumentNullException(nameof(windowsProvider));
            _animalsSerializationService = animalsSerializationService
                ?? throw new ArgumentNullException(nameof(animalsSerializationService));
            _animalsSerializerProvider = animalsSerializerProvider
                ?? throw new ArgumentNullException(nameof(animalsSerializerProvider));
            _animalsFactory = animalsFactory
                ?? throw new ArgumentNullException(nameof(animalsFactory));

            EnabledFileFormats = new ReadOnlyCollection<string>(["json", "xml"]);
            SelectedFileFormat = EnabledFileFormats.First();
            Animals = new ObservableCollection<Animal>();

            AddAnimalCommand = new LambdaCommand(AddAnimal, CanAddAnimal);
            LoadAnimalsCommand = new LambdaCommand(LoadAnimals, CanLoadAnimals);
            SaveAnimalsCommand = new LambdaCommand(SaveAnimals, CanSaveAnimals);
        }


        public IReadOnlyCollection<string> EnabledFileFormats { get; }
        public string SelectedFileFormat { get; set; }


        public ObservableCollection<Animal> Animals { get; }


        public ICommand AddAnimalCommand { get; }
        public ICommand LoadAnimalsCommand { get; }
        public ICommand SaveAnimalsCommand { get; }


        private void AddAnimal(object p) {
            var window = _windowsProvider.AnimalCreationWindow;
            if (window.ShowDialog() == true) {
                var vm = window.DataContext as AnimalCreatorViewModel;
                if (vm is not null) {
                    Animals.Add(_animalsFactory.CreateAnimal(vm.SelectedAnimalType, vm.Name));
                }
            }
        }
        private bool CanAddAnimal(object p) => true;


        private void LoadAnimals(object p) {
            Animals.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Выберите файл для загрузки";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = $"{SelectedFileFormat} files (*.{SelectedFileFormat})|*.{SelectedFileFormat}";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = openFileDialog.FileName;
                    ICollection<Animal> animals = _animalsSerializationService.Deserialize(
                        File.ReadAllText(path),
                        _animalsSerializerProvider.GetSerializer(SelectedFileFormat));
                    foreach (Animal animal in animals) {
                        Animals.Add(animal);
                    }
                }
            }
        }
        private bool CanLoadAnimals(object p) => true;


        private void SaveAnimals(object p) {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.Title = "Выберите место для сохранения";
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                saveFileDialog.Filter = $"{SelectedFileFormat} files (*.{SelectedFileFormat})|*.{SelectedFileFormat}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = saveFileDialog.FileName;
                    string str = _animalsSerializationService.Serialize(
                        Animals,
                        _animalsSerializerProvider.GetSerializer(SelectedFileFormat));
                    File.WriteAllText(path, str);
                }
            }
        }
        private bool CanSaveAnimals(object p) => Animals.Count > 0;
    }
}
