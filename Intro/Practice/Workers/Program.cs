using Workers.Entities;
using Workers.Repositories;
using Workers.Services;

namespace Workers {
    internal class Program {
        private static readonly Repository _repository = new Repository();
        private static readonly Dictionary<string, Command> _commandsByKeyes = new Dictionary<string, Command>() {
            {"help", Command.Help },
            {"1", Command.GetAllWorkers },
            {"2", Command.GetWorkerById },
            {"3", Command.GetWorkersByDateRange },
            {"4", Command.DeleteWorker },
            {"5", Command.AddWorker }
        };


        internal static void Main() {
            try {
                while (true) {
                    Command mode = GetCommand();
                    RunCommand(mode);
                }
            } catch (OperationCanceledException) {
                Console.Clear();
                Console.WriteLine("Для выхода из программы нажмите Enter:");
                Console.ReadKey();
            }
        }


        private static Worker CreateWorker() {
            string fio = GetStringFromUser("Введите ФИО сотрудника:");
            var birthday = GetDateFromUser("Введите дату рождения сотрудника:");
            var homeCity = GetStringFromUser("Введите место рождения сотрудника:");
            var height = GetDoubleFromUser("Введите рост сотрудника в см:");
            return new Worker(fio) {
                Birthday = birthday,
                HomeCity = homeCity,
                Height = height
            };
        }

        private static string GetStringFromUser(string msg) {
            Console.WriteLine($"{msg} (для выхода введите 'q')");
            string? result;
            do {
                result = Console.ReadLine() ?? string.Empty;
                if (result == "q") {
                    throw new OperationCanceledException();
                }
                if (result.Contains('#')) {
                    Console.WriteLine("Нельзя использовать символ '#'!");
                }
            } while (string.IsNullOrWhiteSpace(result) || result.Contains('#'));
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

        private static int GetIntFromUser(string msg) {
            int result;
            bool success;
            do {
                success = int.TryParse(GetStringFromUser(msg), out result);
            } while (!success);
            return result;
        }

        /// <summary>
        /// Возвращает индекс поля для сортировки сотрудников
        /// </summary>
        /// <returns></returns>
        private static int GetSortFieldIndex() {
            var props = Worker.GetFieldIndexes();
            Console.WriteLine($"Доступные поля для сортировки:");
            foreach (var prop in props) {
                Console.WriteLine($"{prop.Key.ToString():-5}\t{prop.Value}");
            }
            int index;
            do {
                index = GetIntFromUser(
                    "Для сортировки по возрастанию введите индекс, " +
                    "для сортировки по убыванию введите отрицательный индекс:");
            } while (!props.Keys.Contains(Math.Abs(index)));
            return index;
        }

        private static void RunCommand(Command command) {
            switch (command) {
                case Command.GetAllWorkers:
                    RunGetAllWorkersCommand();
                    break;
                case Command.GetWorkerById:
                    RunGetWorkerByIdCommand();
                    break;
                case Command.GetWorkersByDateRange:
                    RunGetWorkersByDateRangeCommand();
                    break;
                case Command.DeleteWorker:
                    RunDeleteWorkerCommand();
                    break;
                case Command.AddWorker:
                    RunAddWorkerCommand();
                    break;
                case Command.Help:
                    RunHelpCommand();
                    break;
                default:
                    break;
            }
        }

        private static void RunGetAllWorkersCommand() {
            Console.Clear();
            var workers = _repository.GetAllWorkers();
            if (workers.Length > 1) {
                workers = Worker.SortWorkersByFieldIndex(workers, GetSortFieldIndex());
            }
            Console.WriteLine(string.Join('\n', workers.Select(w => w.ToDisplayString())));
            Console.WriteLine("\nДля продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void RunGetWorkerByIdCommand() {
            Console.Clear();
            int id = GetIntFromUser("Введите Id сотрудника, которого нужно найти:");
            try {
                Console.WriteLine(_repository.GetWorkerById(id).ToDisplayString());
            } catch (InvalidOperationException) {
                Console.WriteLine("Сотрудник не найден.");
            }
            Console.WriteLine("Для продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void RunGetWorkersByDateRangeCommand() {
            Console.Clear();
            DateTime dateFrom = GetDateFromUser("Введите дату начала промежутка выборки:");
            DateTime dateTo = GetDateFromUser("Введите дату конца промежутка выборки:");
            var workers = _repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
            if (workers.Length > 1) {
                workers = Worker.SortWorkersByFieldIndex(workers, GetSortFieldIndex());
            }
            Console.WriteLine(string.Join('\n', workers.Select(w => w.ToDisplayString())));
            Console.WriteLine("\nДля продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void RunDeleteWorkerCommand() {
            Console.Clear();
            int id = GetIntFromUser("Введите Id сотрудника, которого нужно удалить:");
            _repository.DeleteWorker(id);
            Console.WriteLine("Сотрудник успешно удален. Для продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void RunAddWorkerCommand() {
            Console.Clear();
            var worker = CreateWorker();
            _repository.AddWorker(worker);
            Console.Clear();
            Console.WriteLine("Сотрудник успешно добавлен. Для продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static void RunHelpCommand() {
            Console.Clear();
            foreach (var pair in _commandsByKeyes) {
                Console.WriteLine($"{pair.Key:-6}\t{EnumExtension.GetEnumDescription(pair.Value)}");
            }
            Console.WriteLine("\nДля продолжения нажмите Enter:");
            Console.ReadKey();
        }

        private static Command GetCommand() {
            Console.Clear();
            string msg = "Введите команду и нажмите Enter. Чтобы отобразить список команд введите 'help':";
            string? commandName;
            Command command = Command.Help;
            bool repeat = true;
            do {
                commandName = GetStringFromUser(msg);
                try {
                    command = _commandsByKeyes[commandName];
                    repeat = false;
                } catch (KeyNotFoundException) {
                    repeat = true;
                }
            } while (repeat);
            return command;
        }
    }
}
