using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        private static ReadOnlySpan<(char Roman, int Decimal)> Numerals => new[]
        {
            ('C', 100),
            ('D', 500),
            ('I', 1),
            ('L', 50),
            ('M', 1000),
            ('V', 5),
            ('X', 10)
        };

        private static ReadOnlySpan<(int Decimal, string Roman)> AllNumerals => new[]
        {
            (1, "I"),
            (2, "II"),
            (3, "III"),
            (4, "IV"),
            (5, "V"),
            (6, "VI"),
            (7, "VII"),
            (8, "VIII"),
            (9, "IX"),
            (10, "X"),
            (20, "XX"),
            (30, "XXX"),
            (40, "XL"),
            (50, "L"),
            (60, "LX"),
            (70, "LXX"),
            (80, "LXXX"),
            (90, "XC"),
            (100, "C"),
            (200, "CC"),
            (300, "CCC"),
            (400, "CD"),
            (500, "D"),
            (600, "DC"),
            (700, "DCC"),
            (800, "DCCC"),
            (900, "CM"),
            (1000, "M"),
            (2000, "MM"),
            (3000, "MMM")
        };

        public static string ToRoman(int number)
        {
            if (number < 0 || number > 3999)
                throw new IllegalRomanNumberException(number);

            Span<char> numberStr = stackalloc char[4];
            Span<char> result = stackalloc char[16];
            var numerals = AllNumerals;
            var comparer = new RomanDecimalComparer();
            var position = 0;

            number.TryFormat(numberStr, out var written);

            numberStr = numberStr.Slice(0, written);

            for (int i = 0; i < numberStr.Length; i++)
            {
                var value = (numberStr[i] - '0') * (int)Math.Pow(10, numberStr.Length - i - 1);
                var index = numerals.BinarySearch((value, ""), comparer);
                if (index >= 0)
                {
                    var (_, roman) = numerals[index];
                    roman.AsSpan().CopyTo(result.Slice(position));
                    position += roman.Length;
                }

            }

            return ((ReadOnlySpan<char>)result).Trim('\0').ToString();
        }

        public static int FromRoman(ReadOnlySpan<char> roman)
        {
            var result = 0;

            roman = roman.Trim();

            if (roman.IsEmpty)
                return result;

            var lastCharValue = 0;

            var numerals = Numerals;
            var comparer = new RomanNumeralComparer();

            for (int i = roman.Length - 1; i >= 0; i--)
            {
                var c = roman[i];
                var index = numerals.BinarySearch((c, 0), comparer);
                if (index < 0)
                    throw new IllegalRomanNumberException(roman.ToString());

                var value = numerals[index].Decimal;

                if (value < lastCharValue)
                    result -= value;
                else
                    result += value;

                lastCharValue = value;
            }

            return result;
        }

        struct RomanNumeralComparer : IComparer<(char, int)>
        {
            public int Compare((char, int) x, (char, int) y)
            {
                return x.Item1.CompareTo(y.Item1);
            }
        }

        struct RomanDecimalComparer : IComparer<(int, string)>
        {
            public int Compare((int, string) x, (int, string) y)
            {
                return x.Item1.CompareTo(y.Item1);
            }
        }
    }
}
