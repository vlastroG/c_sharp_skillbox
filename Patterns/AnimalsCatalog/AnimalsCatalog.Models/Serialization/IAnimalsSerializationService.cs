namespace AnimalsCatalog.Models.Serialization {
    public interface IAnimalsSerializationService {
        public string Serialize(ICollection<Animal> animals, IAnimalsSerializer serializer);

        public ICollection<Animal> Deserialize(string text, IAnimalsSerializer serializer);
    }
}
