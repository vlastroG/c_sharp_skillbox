namespace AnimalsCatalog.Models {
    public class AnimalsFactory : IAnimalsFactory {
        private readonly IAnimalTypesFactory _animalTypesFactory;

        public AnimalsFactory(IAnimalTypesFactory animalTypesFactory) {
            _animalTypesFactory = animalTypesFactory
                ?? throw new ArgumentNullException(nameof(animalTypesFactory));
        }


        public Animal CreateAnimal(string animalType, string animalName) {
            return new Animal(_animalTypesFactory.CreateAnimalType(animalType), animalName);
        }
    }
}
