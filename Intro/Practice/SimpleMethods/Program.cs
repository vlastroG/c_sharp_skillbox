namespace SimpleMethods {
    internal class Program {
        internal static void Main(string[] args) {
            WriteWords(SplitText(GetStringFromUser("Введите несколько слов, разделенных пробелом:"))); //Задание 1
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
    }
}
