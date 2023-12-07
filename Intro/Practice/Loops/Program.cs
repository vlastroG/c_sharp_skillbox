namespace Loops {
    internal class Program {
        internal static void Main() {
            EvenOddNumber();
        }

        internal static void EvenOddNumber() {
            Console.WriteLine("Введите целое число:");
            string str = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(str, out int number)) {
                Console.WriteLine(number % 2 == 0 ? "Число четное" : "Нечетное число");
            } else {
                Console.WriteLine("Неправильный ввод!");
            }
            Console.ReadKey();
        }
    }
}
