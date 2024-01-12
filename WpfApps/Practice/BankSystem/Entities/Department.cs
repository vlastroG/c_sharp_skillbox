using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Entities {
    internal class Department : Entity {
        public Department() : this("default") { }

        public Department(string name) {
            Name = name;
        }


        public string Name { get; set; }


        [InverseProperty(nameof(Client.Department))]
        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
