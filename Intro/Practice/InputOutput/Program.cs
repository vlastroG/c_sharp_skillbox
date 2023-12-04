namespace InputOutput {
    internal class Program {
        static void Main(string[] args) {
            string fullNameFieldName = "ФИО:";
            string fullName = "Сергей Васильевич Рахманинов";

            string ageFieldName = "Возраст:";
            int age = 20;

            string emailFieldName = "email:";
            string email = "rachmaninoff@yandex.ru";

            string programmingScoreFieldName = "Программирование:";
            double programmingScore = 78.9;

            string mathScoreFieldName = "Математика:";
            double mathScore = 94.2;

            string physicScoreFieldName = "Физика:";
            double physicScore = 87.7;

            string totalFieldName = "Итоговый балл:";
            double total = programmingScore + mathScore + physicScore;

            string averageFieldName = "Средний балл:";
            double average = total / 3;

            string line = new('-', 54);
            string gridLine = new('=', 54);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"{line}\n" +
                $"|{fullNameFieldName,-20}| {fullName,-30}|\n" +
                $"{line}\n" +
                $"|{ageFieldName,-20}| {age,-30}|\n" +
                $"{line}\n" +
                $"|{emailFieldName,-20}| {email,-30}|\n" +
                $"{line}\n" +
                $"|{programmingScoreFieldName,-20}| {programmingScore,-30:0.0}|\n" +
                $"{line}\n" +
                $"|{mathScoreFieldName,-20}| {mathScore,-30:0.0}|\n" +
                $"{line}\n" +
                $"|{physicScoreFieldName,-20}| {physicScore,-30:0.0}|\n" +
                $"{line}");
            Console.ReadKey();
            (int left, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(left, top - 1);
            Console.WriteLine(
                $"{gridLine}\n" +
                $"|{totalFieldName,-20}| {total,-30:0.0}|\n" +
                $"{line}\n" +
                $"|{averageFieldName,-20}| {average,-30:0.00}|\n" +
                $"{line}")
                ;
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
