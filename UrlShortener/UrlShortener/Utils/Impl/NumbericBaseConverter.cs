using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Utils.Impl
{
    public class NumbericBaseConverter : INumbericBaseConverter
    {
        private const string CHARACTER_SET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly char[] BaseChars;
        private readonly Dictionary<char, int> CharValues;

        public NumbericBaseConverter()
        {
            BaseChars = CHARACTER_SET.ToCharArray();
            CharValues = BaseChars
                   .Select((c, i) => new { Char = c, Index = i })
                   .ToDictionary(c => c.Char, c => c.Index);
        }

        public string IntToBase(int value)
        {
            int targetBase = BaseChars.Length;

            char[] buffer;
            int i;

            if (value <= 0)
            {
                long newValue = (long)value + Int32.MaxValue - Int32.MinValue;
                buffer = new char[Math.Max((int)Math.Ceiling(Math.Log(Int32.MaxValue, targetBase)), 1)];

                i = buffer.Length;
                do
                {
                    var remainder = newValue % targetBase;
                    buffer[--i] = BaseChars[remainder];
                    newValue = (long)(newValue / targetBase);
                }
                while (newValue > 0);
            }
            else
            {
                value--;
                buffer = new char[Math.Max((int)Math.Ceiling(Math.Log(value + 1, targetBase)), 1)];

                i = buffer.Length;
                do
                {
                    buffer[--i] = BaseChars[value % targetBase];
                    value = value / targetBase;
                }
                while (value > 0);
            }

            return new string(buffer, i, buffer.Length - i);
        }

        public int BaseToInt(string number)
        {
            char[] chrs = number.ToCharArray();
            int m = chrs.Length - 1;
            int n = BaseChars.Length, x;
            int result = 0;
            for (int i = 0; i < chrs.Length; i++)
            {
                x = CharValues[chrs[i]];
                result += x * (int)Math.Pow(n, m--);
            }
            return result;
        }
    }
}
