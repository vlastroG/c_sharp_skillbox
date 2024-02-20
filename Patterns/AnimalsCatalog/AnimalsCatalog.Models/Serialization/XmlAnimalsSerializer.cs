using System.Xml.Serialization;

namespace AnimalsCatalog.Models.Serialization {
    public class XmlAnimalsSerializer : IXmlAnimalsSerializer {
        private readonly XmlSerializer _serializer;
        public XmlAnimalsSerializer() {
            _serializer = new XmlSerializer(typeof(Animal[]));
        }


        public ICollection<Animal> Deserialize(string str) {
            using (TextReader reader = new StringReader(str)) {
                return _serializer.Deserialize(reader) as ICollection<Animal> ?? Array.Empty<Animal>();
            }
        }


        public string Serialize(ICollection<Animal> animals) {
            string text;
            Animal[] animalsArray = animals.ToArray();
            using (TextWriter writer = new StringWriter()) {
                _serializer.Serialize(writer, animalsArray);
                text = writer.ToString() ?? string.Empty;
            }
            return text;
        }
    }
}
