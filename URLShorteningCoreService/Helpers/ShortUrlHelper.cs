using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShorteningCoreService.Helpers
{
    public class ShortUrlHelper
    {
        public static string GetShortUrl(string value)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < ConvertConstants.MaxNumberOfChars; i++)
            {
                char nextChar = GetNextChar(uint.Parse(value), i);
                stringBuilder.Append(nextChar);
            }

            return stringBuilder.ToString();
        }

        public static int GetId(string shortUrl)
        {
            uint result = 0;

            for (int i = 0; i < shortUrl.Length; i++)
            {
                var charCode = (uint)shortUrl[i];
                uint valueFromChar = GetIntFromCharCode(charCode);
                uint shiftedValue = GetIdShiftValue(valueFromChar, i);
                result += shiftedValue;
            }

            return (int)result;
        }

        private static char GetNextChar(uint value, int index)
        {
            uint shiftedValue = ShiftValue(value, index);
            uint code = GetCharCodeFromInt(shiftedValue);
            return (char)code;
        }

        private static uint ShiftValue(uint value, int index)
        {
            return value >> index * ConvertConstants.NumberOfBitsForOneSymbol << ConvertConstants.MaxNumberOfBits - ConvertConstants.NumberOfBitsForOneSymbol >> ConvertConstants.MaxNumberOfBits - ConvertConstants.NumberOfBitsForOneSymbol;
        }

        private static uint GetCharCodeFromInt(uint shiftedValue)
        {
            if (shiftedValue <= ConvertConstants.MaxShiftedValueForLetters)
            {
                shiftedValue = shiftedValue + ConvertConstants.OffsetToLowLetterInASCII;
            }

            if (shiftedValue > ConvertConstants.MaxShiftedValueForLetters && shiftedValue <= ConvertConstants.MaxShiftedValue)
            {
                shiftedValue =  shiftedValue - ConvertConstants.MinShiftedValueForDigits + ConvertConstants.OffsetToDigitInASCII;
            }

            return shiftedValue;
        }

       

        private static uint GetIntFromCharCode(uint charCode)
        {
            if (charCode >= ConvertConstants.OffsetToLowLetterInASCII && charCode <= ConvertConstants.OffsetToLowLetterInASCII + ConvertConstants.NumberOfLetters)
            {
                charCode = charCode - ConvertConstants.OffsetToLowLetterInASCII;
            }
            if (charCode >= ConvertConstants.OffsetToDigitInASCII && charCode <= ConvertConstants.OffsetToDigitInASCII + ConvertConstants.NumberOfDigit)
            {
                charCode = charCode - ConvertConstants.OffsetToDigitInASCII + ConvertConstants.NumberOfLetters;
            }
            return charCode;
        }

        private static uint GetIdShiftValue(uint value, int index)
        {
            return value << index * ConvertConstants.NumberOfBitsForOneSymbol;
        }
    }
}
