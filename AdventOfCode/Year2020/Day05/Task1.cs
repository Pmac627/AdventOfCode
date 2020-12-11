using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day05
{
    [ExpectedResult("806")]
    public class Task1 : IRunnableCode
    {
        private const int _minRow = 0;
        private const int _maxRow = 127;
        private const int _minCol = 0;
        private const int _maxCol = 7;
        private const int _seatIdMultiple = 8;
        private static readonly (int Start, int Length) _rowSubString = (0, 7);
        private static readonly (int Start, int Length) _columnSubString = (7, 3);

        private const char _frontChar = 'F';
        private const char _leftChar = 'L';

        public async Task<string> ExecuteAsync(string[] data)
        {
            var highest = 0;

            foreach (var code in data)
            {
                var seatId = GetSeatId(code);

                if (seatId > highest)
                {
                    highest = seatId;
                }
            }

            return highest.ToString();
        }

        private static int GetSeatId(string code)
        {
            var row = GetRowNumber(code);
            var col = GetColumnNumber(code);

            row *= _seatIdMultiple;

            return row + col;
        }

        private static int GetRowNumber(string code)
        {
            return GetPosition(code.Substring(_rowSubString.Start, _rowSubString.Length), _minRow, _maxRow, _frontChar);
        }

        private static int GetColumnNumber(string code)
        {
            return GetPosition(code.Substring(_columnSubString.Start, _columnSubString.Length), _minCol, _maxCol, _leftChar);
        }

        private static int GetPosition(string code, int lower, int upper, char upperChar)
        {
            var total = upper + 1;
            var last = code.Length - 1;

            for (var i = 0; i < code.Length; i++)
            {
                if (i == last)
                {
                    if (code[i] == upperChar)
                    {
                        return lower;
                    }

                    return upper;
                }

                total /= 2;

                if (code[i] == upperChar)
                {
                    upper -= total;
                }
                else
                {
                    lower += total;
                }
            }

            return 0;
        }
    }
}