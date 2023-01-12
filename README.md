# Roman Numerals

A library to convert numbers between arabic and roman written in C#

# How To Use

### Converting from roman to arabic

```cs
string roman = "XXVIII";

int arabic = RomanNumeralsConverter.FromRoman(roman);

Console.WriteLine(arabic); // Output is 28.
```

No trimmed strings are accepted correctly

```cs
string roman = "  XX    ";

int arabic = RomanNumeralsConverter.FromRoman(roman);

Console.WriteLine(arabic); // Output is XX.
```

### Converting from arabic to roman

```cs
int arabic = 32;

string roman = RomanNumeralsConverter.ToRoman(arabic);

Console.WriteLine(roman); // Output is XXXII
```

Only numbers between 0 and 3999 are accepted.

```cs
RomanNumeralsConverter.ToRoman(0); // Will return an empty string

RomanNumeralsConverter.ToRoman(-1); // Will throw IllegalRomanNumberException

RomanNumeralsConverter.ToRoman(3999); // Will return MMMCMXCIX

RomanNumeralsConverter.ToRoman(4000); // Will throw IllegalRomanNumberException
```
