namespace AnimalsCatalog.Models.Serialization {
    public interface IAnimalsSerializer {
        string Serialize(ICollection<Animal> animals);

        ICollection<Animal> Deserialize(string str);
    }
}
