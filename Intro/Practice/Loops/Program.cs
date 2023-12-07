namespace Loops {
    internal class Program {
        private const int _maxCardsCount = 52;
        private const int _minNumberCard = 2;
        private const int _maxNumberCard = 10;
        private static readonly char[] _cardSymbols = { 'J', 'Q', 'K', 'T' };

        internal static void Main() {
            //EvenOddNumber(); //Задание 1
            //TwentyOneGame(); //Задание 2
            //PrimeNumber(); //Задание 3
            FindMinimumInput();
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

        internal static void PrimeNumber() {
            int number = GetNumber(1, int.MaxValue);
            int dividersCount = 0;
            int halfNumber = number / 2;
            for (int i = 1; i <= halfNumber; i++) {
                if ((number % i) == 0) {
                    dividersCount++;
                    if (dividersCount > 2) {
                        break;
                    }
                }
            }
            string message = dividersCount <= 2 ? "Число простое" : "Число не простое";
            Console.WriteLine(message);
        }

        internal static void FindMinimumInput() {
            int numbersCount = GetNumber(1, int.MaxValue);
            int minimum = int.MaxValue;
            for (int i = 0; i < numbersCount; i++) {
                Console.WriteLine($"Ввод {i + 1}-го числа:");
                int number = GetNumber(int.MinValue, int.MaxValue);
                minimum = number < minimum ? number : minimum;
            }
            Console.WriteLine($"Минимальное число в введенной последовательности = {minimum}");
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
            bool askForNumber = true;
            int number = 0;
            while (askForNumber) {
                Console.WriteLine(message);
                str = Console.ReadLine() ?? string.Empty;
                try {
                    number = int.Parse(str);
                    if (min <= number && number <= max) {
                        askForNumber = false;
                    }
                } catch (ArgumentNullException) {
                    continue;
                } catch (FormatException) {
                    continue;
                } catch (OverflowException) {
                    continue;
                }
            }
            return number;
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
