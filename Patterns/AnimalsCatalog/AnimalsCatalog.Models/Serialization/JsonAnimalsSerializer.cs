using Newtonsoft.Json;

namespace AnimalsCatalog.Models.Serialization {
    public class JsonAnimalsSerializer : IJsonAnimalsSerializer {
        public ICollection<Animal> Deserialize(string str) {
            return JsonConvert.DeserializeObject<ICollection<Animal>>(str, new JsonSerializerSettings() {
                Converters = new[] { new AnimalTypeConverter() }
            }) ?? Array.Empty<Animal>();
        }

        public string Serialize(ICollection<Animal> animals) {
            return JsonConvert.SerializeObject(animals, new AnimalTypeConverter());
        }
    }

    public class AnimalTypeConverter : JsonConverter<AnimalType> {
        public override AnimalType? ReadJson(
            JsonReader reader,
            Type objectType,
            AnimalType? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer) {

            switch (reader.Value) {
                case "Млекопитающие":
                    return new Mammal();
                case "Птицы":
                    return new Bird();
                case "Земноводные":
                    return new Amphibian();
                default:
                    return new UnknownAnimalType();
            }
        }


        public override void WriteJson(JsonWriter writer, AnimalType? value, JsonSerializer serializer) {
            writer.WriteValue(value.Name);
        }
    }
}
