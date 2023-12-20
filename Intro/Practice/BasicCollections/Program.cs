using System.Xml.Linq;

namespace BasicCollections {
    internal class Program {
        internal static void Main() {
            //ListSample(); // Задание 1
            //PhoneBook(); // Задание 2
            //CheckDuplicates(); // Задание 3
            CreateAndSavePerson(); //Задание 4
        }


        private static void ListSample() {
            Console.WriteLine("Случайные числа до фильтрации:\n");
            List<int> list = CreateRandomNumbers();
            PrintList(list);

            Console.WriteLine("\nНажмите Enter:");
            Console.ReadKey();

            Console.WriteLine("Те же числа, но в диапазоне [0; 25] и [50; 100]:");
            PrintList(FilterNumbers(list));
            Console.ReadKey();
        }

        private static List<int> CreateRandomNumbers() {
            Random rnd = new Random();
            return Enumerable.Range(0, 100).Select(i => rnd.Next(0, 101)).ToList();
        }

        private static void PrintList(List<int> list) {
            list.Chunk(5).ToList().ForEach(numbers => Console.WriteLine(string.Join('\t', numbers)));
        }

        private static List<int> FilterNumbers(List<int> list) {
            return list.Where(item => item <= 25 || 50 <= item).ToList();
        }


        private static void PhoneBook() {
            Dictionary<string, string> phoneBook = CreatePhoneBook();
            Console.WriteLine("\nВведите номер телефона для поиска:");
            string phoneNumber = Console.ReadLine() ?? string.Empty;
            Console.WriteLine(
                phoneBook.TryGetValue(phoneNumber, out string? fullName)
                ? fullName
                : "Пользователь с заданным телефоном отсутствует!");
        }

        private static Dictionary<string, string> CreatePhoneBook() {
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            try {
                string phoneNumber;
                string fullName;
                bool success;
                while (true) {
                    phoneNumber = GetPhoneBookDataFromUser("Введите номер телефона:");
                    success = phoneBook.TryAdd(phoneNumber, string.Empty);
                    if (!success) {
                        Console.WriteLine("Такой номер телефона уже существует!");
                        continue;
                    }
                    fullName = GetPhoneBookDataFromUser($"Введите ФИО владельца телефона {phoneNumber}");
                    phoneBook[phoneNumber] = fullName;
                }
            } catch (OperationCanceledException) {
                var emptyPairs = phoneBook.Where(pair => pair.Value == string.Empty).ToArray();
                foreach (var pair in emptyPairs) {
                    phoneBook.Remove(pair.Key);
                }
            }
            return phoneBook;
        }

        private static string GetPhoneBookDataFromUser(string msg) {
            Console.WriteLine($"{msg} (для прекращения ввода введите 'q')");
            string? result;
            do {
                result = Console.ReadLine();
                if (result == "q") {
                    throw new OperationCanceledException();
                }
            } while (string.IsNullOrWhiteSpace(result));
            return result;
        }

        private static void CheckDuplicates() {
            try {
                HashSet<int> numbers = new HashSet<int>();
                int number;
                while (true) {
                    number = GetIntFromUser($"Введите число в диапазоне [{int.MinValue}; {int.MaxValue}]:");
                    if (!numbers.Add(number)) {
                        Console.WriteLine("Такое число уже вводилось, введите другое!");
                    }
                }
            } catch (OperationCanceledException) {
            }
        }

        private static string GetStringFromUser(string msg) {
            Console.WriteLine($"{msg} (для выхода введите 'q')");
            string? result;
            do {
                result = Console.ReadLine() ?? string.Empty;
                if (result == "q") {
                    throw new OperationCanceledException();
                }
            } while (string.IsNullOrWhiteSpace(result));
            return result;
        }

        private static int GetIntFromUser(string msg) {
            int result;
            bool success;
            do {
                success = int.TryParse(GetStringFromUser(msg), out result);
            } while (!success);
            return result;
        }

        private static XElement CreatePerson() {
            XElement xPerson = new XElement("Person");
            xPerson.SetAttributeValue("name", GetStringFromUser("Введите ФИО человека:"));

            XElement xStreet = new XElement("Street", GetStringFromUser("Введите название улицы:"));
            XElement xHouseNumber = new XElement("HouseNumber", GetStringFromUser("Введите номер дома:"));
            XElement xFlatNumber = new XElement("FlatNumber", GetStringFromUser("Введите номер квартиры:"));
            XElement xAddress = new XElement("Address",
                xStreet,
                xHouseNumber,
                xFlatNumber
                );
            xPerson.Add(xAddress);

            XElement xMobilePhone = new XElement("MobilePhone", GetStringFromUser("Введите номер мобильного телефона:"));
            XElement xFlatPhone = new XElement("FlatPhone", GetStringFromUser("Введите номер домашнего телефона:"));
            XElement xPhones = new XElement("Phones",
                xMobilePhone,
                xFlatPhone
                );
            xPerson.Add(xPhones);

            return xPerson;
        }

        private static void SerializePerson(XElement person) {
            using (Stream stream = new FileStream("./person.xml", FileMode.Create, FileAccess.Write)) {
                person.Save(stream);
                stream.Close();
            }
        }

        private static void CreateAndSavePerson() {
            try {
                XElement person = CreatePerson();
                SerializePerson(person);
                Console.WriteLine("Данные успешно сохранены.");
            } catch (OperationCanceledException) {
            }
            Console.WriteLine("Для выхода из программы нажмите Enter:");
            Console.ReadLine();
        }
    }
}
