using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Workers.Entities {
    internal struct Worker {
        public Worker(string fio) {
            if (string.IsNullOrWhiteSpace(fio)) {
                throw new ArgumentException($"'{nameof(fio)}' cannot be null or whitespace.", nameof(fio));
            }

            RecordDate = DateTime.Now;
            FIO = fio;
        }

        [Display(Description = "Id", Order = 0)]
        public int Id { get; set; }

        [Display(Description = "Дата записи", Order = 1)]
        public DateTime RecordDate { get; private set; }

        [Display(Description = "ФИО", Order = 2)]
        public string FIO { get; set; } = string.Empty;

        [Display(Description = "Возраст", Order = 3)]
        public int Age {
            get {
                var now = DateTime.Now;
                return (now.Year - Birthday.Year)
                    + ((Birthday.Month > now.Month)
                    || (Birthday.Month == now.Month && Birthday.Day > now.Day) ? (-1) : 0);
            }
        }

        [Display(Description = "Рост", Order = 4)]
        public double Height { get; set; }

        [Display(Description = "День рождения", Order = 5)]
        public DateTime Birthday { get; set; }

        [Display(Description = "Место рождения", Order = 6)]
        public string HomeCity { get; set; } = string.Empty;


        public override string ToString() {
            return $"{Id}#{RecordDate:dd.MM.yyyy HH:mm}#{FIO}#{Age}#{Height}#{Birthday:dd.MM.yyyy}#{HomeCity}";
        }

        public string ToDisplayString() {
            return $"{Id} {RecordDate:dd.MM.yyyy HH:mm} {FIO} {Age} {Height} {Birthday:dd.MM.yyyy} {HomeCity}";
        }

        /// <summary>
        /// Возвращает словарь, в котором ключи - индекс свойства типа <see cref="Worker"/>, 
        /// а значение - описание этого свойства
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, string> GetFieldIndexes() {
            return GetPropertiesDisplayAttributes()
                .Select(attr => new { Order = attr.Order, Description = attr.Description! })
                .ToDictionary(attr => attr.Order, attr => attr.Description);
        }

        /// <summary>
        /// Конвертирует строку в экземпляр <see cref="Worker"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Исключение, если что-то пошло не по плану</exception>
        public static Worker ConvertFromString(string? str) {
            string[] workerData = str?.Split('#') ?? Array.Empty<string>();
            var properties = typeof(Worker)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(p => p.GetCustomAttribute<DisplayAttribute>()?.Order)
                .ToArray();
            if ((workerData is null) || (workerData.Length != properties.Length)) {
                throw new ArgumentException(nameof(str));
            } else {
                try {
                    return new Worker() {
                        Id = int.Parse(workerData[0]),
                        RecordDate = DateTime.Parse(workerData[1]),
                        FIO = workerData[2],
                        Height = int.Parse(workerData[4]),
                        Birthday = DateTime.Parse(workerData[5]),
                        HomeCity = workerData[6]
                    };
                } catch (Exception ex) when
                (ex is ArgumentNullException || ex is FormatException || ex is OverflowException) {
                    throw new ArgumentException(nameof(str));
                }
            }
        }

        /// <summary>
        /// Сортирует массив сотрудников по индексу заданного поля.
        /// Для сортировки по возрастанию индекс >=0, для сортировки по убыванию индекс <0.
        /// </summary>
        /// <param name="workers">Массив сотрудников для сортировки</param>
        /// <param name="index">Значение индекса свойства, полученного с помощью <see cref="GetFieldIndexes"/></param>
        /// <returns></returns>
        public static Worker[] SortWorkersByFieldIndex(Worker[] workers, int index) {
            var property = typeof(Worker)
                .GetProperties()
                .FirstOrDefault(prop => prop.GetCustomAttribute<DisplayAttribute>(false)?.Order == Math.Abs(index));
            if (property != null) {
                if (index >= 0) {
                    return workers.OrderBy(worker => property.GetValue(worker)).ToArray();
                } else {
                    return workers.OrderByDescending(worker => property.GetValue(worker)).ToArray();
                }
            } else {
                return workers;
            }
        }


        /// <summary>
        /// Возвращает массив всех атрибутов <see cref="DisplayAttribute"/> у свойств
        /// </summary>
        /// <returns></returns>
        private static DisplayAttribute[] GetPropertiesDisplayAttributes() {
            return typeof(Worker)
                .GetProperties()
                .Select(p => p.GetCustomAttribute<DisplayAttribute>(false))
                .Where(attr => attr != null)
                .Cast<DisplayAttribute>()
                .ToArray();
        }
    }
}
