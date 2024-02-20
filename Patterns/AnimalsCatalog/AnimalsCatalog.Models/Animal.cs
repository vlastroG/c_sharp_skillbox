namespace AnimalsCatalog.Models {

    public class Animal {
        private Animal() {
            AnimalType = new UnknownAnimalType();
            Name = "default";
        }


        public Animal(AnimalType animalType, string name) {
            AnimalType = animalType;
            Name = name;
        }


        public string Name { get; set; }

        public AnimalType AnimalType { get; set; }
    }
}
