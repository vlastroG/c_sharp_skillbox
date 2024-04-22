using System.Globalization;

namespace CustomConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double number = DoubleConverter.Convert(args[0]);
            Console.WriteLine($"My           --> {number}");
            Console.WriteLine($"double.Parse --> {double.Parse(args[0], CultureInfo.InvariantCulture)}");
        }
    }
}
