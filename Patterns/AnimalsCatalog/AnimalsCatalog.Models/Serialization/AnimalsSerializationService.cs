
namespace AnimalsCatalog.Models.Serialization {
    public class AnimalsSerializationService : IAnimalsSerializationService {
        public string Serialize(ICollection<Animal> animals, IAnimalsSerializer serializer) {
            return serializer.Serialize(animals);
        }

        public ICollection<Animal> Deserialize(string text, IAnimalsSerializer serializer) {
            return serializer.Deserialize(text);
        }
    }
}
