namespace AnimalsCatalog.Models {
    public class AnimalTypesFactory : IAnimalTypesFactory {
        public AnimalTypesFactory() {

        }

        public AnimalType CreateAnimalType(string animalType) {
            return animalType switch {
                "Млекопитающие" => new Mammal(),
                "Птицы" => new Bird(),
                "Земноводные" => new Amphibian(),
                _ => new UnknownAnimalType()
            };
        }
    }
}
