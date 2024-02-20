namespace AnimalsCatalog.Models.Serialization {
    public interface IAnimalsSerializerProvider {
        IAnimalsSerializer GetSerializer(string fileFormat);
    }
}
