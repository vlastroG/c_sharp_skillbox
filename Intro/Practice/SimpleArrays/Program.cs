namespace SimpleArrays {
    internal class Program {
        internal static void Main() {
            //RandomMatrix(); //Задание 1
            SumOfRundomMatrix(); //Задание 2
        }


        private static void SumOfRundomMatrix() {
            int minSize = 1;
            int maxSize = 20;
            int x = GetNumber($"Введите ширину матриц от {minSize} до {maxSize}", minSize, maxSize);
            int y = GetNumber($"Введите высоту матриц от {minSize} до {maxSize}", minSize, maxSize);

            Console.WriteLine("Матрица 1:");
            int[,] matrix1 = GetMatrix(y, x);
            DrawMatrix(matrix1);

            Console.WriteLine("\nМатрица 2:");
            int[,] matrix2 = GetMatrix(y, x);
            DrawMatrix(matrix2);

            Console.WriteLine("\nСуммарная матрица:");
            long[,] sumMatrix = SumMatrix(matrix1, matrix2);
            DrawMatrix(sumMatrix);
        }

        private static long[,] SumMatrix(int[,] matrix1, int[,] matrix2) {
            if (matrix1.GetLength(0) != matrix2.GetLength(0)
                || matrix1.GetLength(1) != matrix2.GetLength(1)) {
                throw new ArgumentException("Размер матриц должны быть одинаковые!");
            }
            int x = matrix1.GetLength(0);
            int y = matrix1.GetLength(1);
            long[,] result = new long[x, y];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    result[i, j] = (long)matrix1[i, j] + (long)matrix2[i, j];
                }
            }
            return result;
        }

        private static void RandomMatrix() {
            int minSize = 1;
            int maxSize = 20;
            int x = GetNumber($"Введите ширину матрицы от {minSize} до {maxSize}", minSize, maxSize);
            int y = GetNumber($"Введите высоту матрицы от {minSize} до {maxSize}", minSize, maxSize);
            var matrix = GetMatrix(y, x);
            DrawMatrix(matrix);
            Console.WriteLine($"Сумма чисел в матрице = {GetSum(matrix)}");
        }

        private static void DrawMatrix(long[,] matrix) {
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    Console.Write($"{matrix[i, j],-12}");
                }
                Console.WriteLine();
            }
        }

        private static void DrawMatrix(int[,] matrix) {
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    Console.Write($"{matrix[i, j],-12}");
                }
                Console.WriteLine();
            }
        }

        private static long GetSum(int[,] matrix) {
            long sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    sum += matrix[i, j];
                }
            }
            return sum;
        }


        /// <summary>
        /// Возвращает матрицу с рандомными числами
        /// </summary>
        /// <param name="x">Высота</param>
        /// <param name="y">Ширина</param>
        /// <returns></returns>
        private static int[,] GetMatrix(int x, int y) {
            int[,] matrix = new int[x, y];
            Random rnd = new Random();
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    matrix[i, j] = rnd.Next(int.MinValue, int.MaxValue);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Возвращает целое число, введенное пользователем в заданном диапазоне, включая минимум и максимум
        /// </summary>
        /// <param name="msg">Сообщение для пользователя</param>
        /// <param name="min">Минимум</param>
        /// <param name="max">Максимум</param>
        /// <returns></returns>
        private static int GetNumber(string msg, int min, int max) {
            string str;
            bool askForNumber = true;
            int number = 0;
            while (askForNumber) {
                Console.WriteLine(msg);
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
    }
}
