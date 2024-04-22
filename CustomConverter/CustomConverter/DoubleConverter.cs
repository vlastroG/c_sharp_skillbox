namespace CustomConverter
{
    public static class DoubleConverter
    {
        private static readonly char[] _numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] _splitters = new[] { '.', ',' };
        private const char _negate = '-';
        private const char _zero = '0';
        private const int _numbersAsciiStart = 48;


        /// <summary>
        /// Конвертирует строковое представление double числа в double.
        /// Валидные представления:
        /// ХХХ.ХХХ ;
        /// ХХХ,ХХХ ;
        /// ХХХ. ;
        /// ХХХ, ;
        /// .ХХХ ;
        /// ,ХХХ ;
        /// ХХХ ;
        /// -ХХХ ;
        /// -ХХХ. ;
        /// -ХХХ, ;
        /// -ХХХ.ХХХ ;
        /// -ХХХ,ХХХ
        /// где Х - цифра от 0 до 9 включительно
        /// </summary>
        /// <param name="str">Строковое представление double числа</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static double Convert(string str)
        {
            if (str is null) { throw new ArgumentNullException(nameof(str)); }
            if (str.Length == 0) { return 0; }

            if (str[0] == _negate && str.Length > 1 && CharIsNumber(str[1]))
            {
                return ConvertNegativeDouble(str);
            } else if (CharIsSplitter(str[0]) || CharIsNumber(str[0]))
            {
                return ConvertPositiveDouble(str);
            } else
            {
                throw new ArgumentException(nameof(str));
            }
        }

        private static double ConvertNegativeDouble(string str)
        {
            if (str is null || str.Length < 2 || str[0] != _negate || !CharIsNumber(str[1]))
            {
                throw new ArgumentException();
            }

            int floatPower = -1;
            double result = 0;
            bool splitterFound = false;
            for (int i = 1; i < str.Length; i++)
            {
                if (!splitterFound && CharIsNumber(str[i]))
                {
                    result *= 10;
                    result += (str[i] - _numbersAsciiStart);
                } else if (!splitterFound && CharIsSplitter(str[i]))
                {
                    splitterFound = true;
                } else if (splitterFound && CharIsNumber(str[i]))
                {
                    result += (str[i] - _numbersAsciiStart) * PowerNumber(10, floatPower--);
                } else
                {
                    throw new ArgumentException();
                }
            }
            return -result;
        }

        private static double ConvertPositiveDouble(string str)
        {
            if (str is null) { throw new ArgumentException(); }

            double result = 0;
            int floatPower = -1;
            bool splitterFound = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!splitterFound && CharIsNumber(str[i]))
                {
                    result *= 10;
                    result += (str[i] - _numbersAsciiStart);
                } else if (!splitterFound && CharIsSplitter(str[i]))
                {
                    splitterFound = true;
                } else if (splitterFound && CharIsNumber(str[i]))
                {
                    result += (str[i] - _numbersAsciiStart) * PowerNumber(10, floatPower--);
                } else
                {
                    throw new ArgumentException();
                }
            }
            return result;
        }

        private static double PowerNumber(double value, int power)
        {
            double result = 1;
            if (power < 0)
            {
                for (int i = 0; i < -power; i++)
                {
                    result /= value;
                }
            }
            if (power == 0)
            {
                result = 1;
            } else
            {
                for (int i = 0; i < power; i++)
                {
                    result *= value;
                }
            }
            return result;
        }


        private static bool CharIsNumber(char c)
        {
            foreach (char digit in _numbers)
            {
                if (c == digit) { return true; }
            }
            return false;
        }

        private static bool CharIsSplitter(char c)
        {
            foreach (char splitter in _splitters)
            {
                if (c == splitter) { return true; }
            }
            return false;
        }

        private static char[] StringToArray(string str)
        {
            if (str == null) { throw new ArgumentNullException(nameof(str)); }

            char[] array = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                array[i] = str[i];
            }
            return array;
        }
    }
}
