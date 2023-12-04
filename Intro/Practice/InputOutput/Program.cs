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

            string line = new string('-', 54);

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
                $"{line}\n")
                ;
            Console.ReadKey();
        }
    }
}
