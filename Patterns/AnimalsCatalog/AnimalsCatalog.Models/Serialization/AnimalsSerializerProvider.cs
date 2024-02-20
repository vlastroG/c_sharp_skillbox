namespace AnimalsCatalog.Models.Serialization {
    public class AnimalsSerializerProvider : IAnimalsSerializerProvider {
        public AnimalsSerializerProvider() {

        }


        public IAnimalsSerializer GetSerializer(string fileFormat) {
            return fileFormat switch {
                "json" => new JsonAnimalsSerializer(),
                "xml" => new XmlAnimalsSerializer(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
