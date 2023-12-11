namespace SimpleMethods {
    internal class Program {
        internal static void Main(string[] args) {
            //WriteWords(SplitText(GetStringFromUser("Введите несколько слов, разделенных пробелом:"))); //Задание 1
            ReversWords(GetStringFromUser("Введите фразу, слова которой нужно написать в обратном порядке:"));//Задание 2
        }


        private static string[] SplitText(string? text) {
            return text?.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        }

        private static void WriteWords(string[] words) {
            Array.ForEach(words, (words) => Console.WriteLine(string.Join("\n", words)));
        }

        private static string GetStringFromUser(string msg) {
            Console.WriteLine($"{msg}");
            return Console.ReadLine() ?? string.Empty;
        }

        private static string Reverse(string text) {
            return string.Join(' ', SplitText(text).Reverse());
        }

        private static void ReversWords(string inputPhrase) {
            Console.WriteLine(Reverse(inputPhrase));
        }
    }
}
