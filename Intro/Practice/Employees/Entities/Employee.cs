namespace Employees.Entities {
    internal class Employee {
        private static long _lastId = 1;


        public Employee(string surname, string name, string patronymic) {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(surname)) {
                throw new ArgumentException($"'{nameof(surname)}' cannot be null or whitespace.", nameof(surname));
            }

            if (string.IsNullOrWhiteSpace(patronymic)) {
                throw new ArgumentException($"'{nameof(patronymic)}' cannot be null or whitespace.", nameof(patronymic));
            }

            ID = _lastId++;
            RecordDate = DateTime.Now;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }


        public long ID { get; }

        public DateTime RecordDate { get; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public int Age {
            get {
                var now = DateTime.Now;
                return (now.Year - Birthday.Year)
                    + ((Birthday.Month > now.Month)
                    || (Birthday.Month == now.Month && Birthday.Day > now.Day) ? (-1) : 0);
            }
        }

        public DateTime Birthday { get; set; }

        public string HomeCity { get; set; } = string.Empty;

        public double Height { get; set; }


        public override string ToString() {
            return $"{ID}#{RecordDate:dd.MM.yyyy HH:mm}#{Surname} {Name} {Patronymic}#{Age}#{Height}#{Birthday:dd.MM.yyyy}#{HomeCity}";
        }
    }
}
