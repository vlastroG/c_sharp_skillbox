using System.ComponentModel.DataAnnotations;

namespace BankSystem.Entities {
    internal abstract class Entity {
        protected Entity() { }

        [Key]
        public int Id { get; set; }
    }
}
