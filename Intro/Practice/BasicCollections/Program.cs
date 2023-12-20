namespace BasicCollections {
    internal class Program {
        internal static void Main() {
            ListSample();
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
    }
}
