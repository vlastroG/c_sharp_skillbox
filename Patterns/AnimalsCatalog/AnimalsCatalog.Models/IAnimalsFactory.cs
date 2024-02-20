namespace AnimalsCatalog.Models {
    public interface IAnimalsFactory {
        Animal CreateAnimal(string animalType, string animalName);
    }
}
