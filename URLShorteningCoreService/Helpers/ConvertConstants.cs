using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShorteningCoreService.Helpers
{
    public class ConvertConstants
    {
        public const int MaxNumberOfChars = 7;
        public const int MaxNumberOfBits = 32;
        public const int NumberOfBitsForOneSymbol = 5;
        public const int MaxShiftedValueForLetters = 25;
        public const int MinShiftedValueForDigits = 26;
        public const int NumberOfLetters = 26;
        public const int NumberOfDigit = 5;
        public const int MaxShiftedValue = 31;
        public const int OffsetToLowLetterInASCII = 97;
        public const int OffsetToDigitInASCII = 48;
    }
}
