using Employees.Entities;
using Employees.Repositories;

namespace Employees {
    internal class Program {
        private static readonly EmployeesRepository _repository = new EmployeesRepository();


        internal static void Main() {
            try {
                while (true) {
                    Mode mode = GetMode();
                    switch (mode) {
                        case Mode.Append:
                            AppendEmployees();
                            break;
                        case Mode.GetData:
                            GetEmployees();
                            break;
                        default:
                            throw new OperationCanceledException();
                    }
                }
            } catch (OperationCanceledException) {
                Console.Clear();
                Console.WriteLine("Для выхода из программы нажмите Enter:");
                Console.ReadKey();
            }
        }


        private static void GetEmployees() {
            Console.Clear();
            Console.WriteLine(_repository.GetAll().Replace('#', ' '));
            Console.WriteLine("\nДля продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void AppendEmployees() {
            Console.Clear();
            var employee = CreateEmployee();
            _repository.Add(employee);
            Console.Clear();
            Console.WriteLine("Сотрудник успешно добавлен. Для продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static Employee CreateEmployee() {
            string surname = GetStringFromUser("Введите фамилию сотрудника:");
            string name = GetStringFromUser("Введите имя сотрудника:");
            string patronymic = GetStringFromUser("Введите отчество сотрудника:");
            var birthday = GetDateFromUser("Введите дату рождения сотрудника:");
            var homeCity = GetStringFromUser("Введите город сотрудника:");
            var height = GetDoubleFromUser("Введите рост сотрудника в см:");
            return new Employee(surname, name, patronymic) {
                Birthday = birthday,
                HomeCity = homeCity,
                Height = height
            };
        }

        private static string GetStringFromUser(string msg) {
            Console.WriteLine($"{msg} (для отмены введите 'q')");
            string? result;
            do {
                result = Console.ReadLine();
                if (result == "q") {
                    throw new OperationCanceledException();
                }
            } while (string.IsNullOrWhiteSpace(result));
            return result;
        }

        private static DateTime GetDateFromUser(string msg) {
            DateTime result;
            bool success;
            do {
                success = DateTime.TryParse(GetStringFromUser(msg), out result);
            } while (!success);
            return result;
        }

        private static double GetDoubleFromUser(string msg) {
            double result;
            bool success;
            do {
                success = double.TryParse(GetStringFromUser(msg), out result);
            } while (!success);
            return result;
        }

        private static Mode GetMode() {
            Console.Clear();
            string msg = "Для вывода данных на экран введите '1', для добавления сотрудников введите '2'";
            string? mode;
            do {
                mode = GetStringFromUser(msg);
            } while (mode != "1" && mode != "2");
            return mode == "1" ? Mode.GetData : Mode.Append;
        }
    }
}

