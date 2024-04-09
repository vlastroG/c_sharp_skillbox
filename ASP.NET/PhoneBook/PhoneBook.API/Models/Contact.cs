using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.API.Models
{
    public class Contact : IValidatableObject
    {
        public Contact()
        {
            Surname = "default";
            Name = "default";
            Patronymic = "default";
            PhoneNumber = "default";
            Address = "default";
            Description = "default";
        }


        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        [MaxLength(50)]
        public string? Patronymic { get; set; }

        [DisplayName("Телефон")]
        [MaxLength(25)]
        public string PhoneNumber { get; set; }

        [DisplayName("Адрес")]
        [MaxLength(255)]
        public string? Address { get; set; }

        [DisplayName("Описание")]
        [MaxLength(255)]
        public string? Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var properties = GetType().GetProperties().Where(p => p.GetAccessors().Any(a => a.IsPublic) && p.PropertyType.Equals(typeof(string)));
            foreach (var field in properties)
            {
                string str = field.GetValue(this) as string ?? string.Empty;
                if (string.IsNullOrWhiteSpace(str))
                {
                    yield return new ValidationResult("Поле не может быть пустым", new[] { field.Name });
                } else if (str.Length != str.Trim().Length)
                {
                    yield return new ValidationResult("Поле не может начинаться или заканчиваться пробелом", new[] { field.Name });
                }
            }
        }
    }
}
