using System;

namespace RomanNumerals
{
    public class IllegalRomanNumberException : Exception
    {
        public int Number { get; }
        public string? Roman { get; }

        public IllegalRomanNumberException(int number)
        {
            Number = number;
        }

        public IllegalRomanNumberException(string roman)
        {
            Roman = roman;
        }

        public IllegalRomanNumberException()
        {

        }
    }
}
