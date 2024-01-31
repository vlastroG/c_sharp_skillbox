using System.ComponentModel.DataAnnotations;

namespace BankSystem.Data.Entities {
    public abstract class Entity {
        protected Entity() { }

        [Key]
        public int Id { get; set; }
    }
}
