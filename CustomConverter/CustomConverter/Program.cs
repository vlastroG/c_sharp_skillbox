namespace CustomConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double number = DoubleConverter.Convert(args[0]);
            Console.WriteLine(number);
        }
    }
}
