namespace Loops {
    internal class Program {
        private const int _maxCardsCount = 52;
        private const int _minNumberCard = 2;
        private const int _maxNumberCard = 10;
        private static readonly char[] _cardSymbols = { 'J', 'Q', 'K', 'T' };

        internal static void Main() {
            //EvenOddNumber(); //Задание 1
            TwentyOneGame(); //Задание 2
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

        internal static void TwentyOneGame() {
            int cardsCount = GetNumber(0, _maxCardsCount);
            int score = 0;
            for (int i = 0; i < cardsCount; i++) {
                score += GetCard(i + 1);
            }
            Console.WriteLine($"Ваш итоговый счет: {score}");
            Console.ReadKey();
        }


        /// <summary>
        /// Возвращает целое число, введенное пользователем в заданном диапазоне, включая минимум и максимум
        /// </summary>
        /// <param name="min">Минимум</param>
        /// <param name="max">Максимум</param>
        /// <returns></returns>
        private static int GetNumber(int min, int max) {
            string message = $"Введите целое число в диапазоне от {min} до {max} включительно:";
            string str;
            int count = -1;
            while (count < min || max < count) {
                Console.WriteLine(message);
                str = Console.ReadLine() ?? string.Empty;
                int.TryParse(str, out int number);
                count = number;
            }
            return count;
        }

        private static int GetCard(int cardNumber) {
            char cardSymbol = '_';
            string str;
            while (true) {
                Console.WriteLine(
                    $"Карта №{cardNumber}. Введите значение от {_minNumberCard} до {_maxNumberCard} " +
                    $"или символ карты из перечня \'{string.Join(",", _cardSymbols)}\':");
                str = Console.ReadLine() ?? string.Empty;
                cardSymbol = str.Length == 1 ? str[0] : cardSymbol;
                if (_cardSymbols.Contains(cardSymbol)) {
                    return GetScore(cardSymbol);
                }
                if (int.TryParse(str, out int number) && _minNumberCard <= number && number <= _maxNumberCard) {
                    return number;
                }
            }
        }

        private static int GetScore(char symbol) {
            int number;
            switch (symbol) {
                case 'J':
                case 'Q':
                case 'K':
                case 'T':
                    number = 10;
                    break;
                default:
                    number = 0;
                    break;
            };
            return number;
        }
    }
}
