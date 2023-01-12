namespace RomanNumerals.Tests
{
    public class RomanNumeralsConverterTests
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "I")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(8, "VIII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(12, "XII")]
        [InlineData(20, "XX")]
        [InlineData(50, "L")]
        [InlineData(100, "C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        [InlineData(2300, "MMCCC")]
        [InlineData(3999, "MMMCMXCIX")]
        public void ToRoman_CompareResult_SucessConverter(int number, string roman)
        {
            var converted = RomanNumeralsConverter.ToRoman(number);

            Assert.Equal(roman, converted);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4000)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void ToRoman_ThrowException(int number)
        {
            Assert.Throws<IllegalRomanNumberException>(() => RomanNumeralsConverter.ToRoman(number));
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "I")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(8, "VIII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(12, "XII")]
        [InlineData(20, "XX    ")]
        [InlineData(50, "L")]
        [InlineData(100, " C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        [InlineData(2300, "MMCCC")]
        [InlineData(3999, "    MMMCMXCIX    ")]
        public void FromRoman_CompareResult_SucessConverter(int number, string roman)
        {
            var converted = RomanNumeralsConverter.FromRoman(roman);

            Assert.Equal(number, converted);
        }

        [Theory]
        [InlineData("A")]
        [InlineData(" XXFF ")]
        public void FromRoman_ThrowException(string roman)
        {
            Assert.Throws<IllegalRomanNumberException>(() => RomanNumeralsConverter.FromRoman(roman));
        }

        [Fact]
        public void CompareResults()
        {
            for (int i = 0; i < 4000; i++)
            {
                var roman = RomanNumeralsConverter.ToRoman(i);
                var number = RomanNumeralsConverter.FromRoman(roman);

                Assert.Equal(i, number);
            }
        }
    }
}