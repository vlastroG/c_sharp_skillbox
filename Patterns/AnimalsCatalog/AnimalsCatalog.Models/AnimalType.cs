using System.Xml.Serialization;

namespace AnimalsCatalog.Models {
    [XmlInclude(typeof(Mammal))]
    [XmlInclude(typeof(Amphibian))]
    [XmlInclude(typeof(Bird))]
    [XmlInclude(typeof(UnknownAnimalType))]
    public abstract class AnimalType {
        protected AnimalType(string str) { Name = str; }

        public string Name { get; }
    }
}
