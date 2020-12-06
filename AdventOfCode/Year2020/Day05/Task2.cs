using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day05
{
    public class Task2 : IRunnableCode
    {
        private const int _minRow = 0;
        private const int _maxRow = 127;
        private const int _minCol = 0;
        private const int _maxCol = 7;
        private const int _seatIdMultiple = 8;
        private const int _seatIdGap = 2;
        private static readonly (int Start, int Length) _rowSubString = (0, 7);
        private static readonly (int Start, int Length) _columnSubString = (7, 3);

        private const char _frontChar = 'F';
        private const char _leftChar = 'L';

        public async Task<string> ExecuteAsync(string[] data)
        {
            var seatIds = new List<int>();

            foreach (var code in data)
            {
                seatIds.Add(GetSeatId(code));
            }

            var sortedSeatIds = seatIds.OrderBy(x => x).ToList();
            var last = sortedSeatIds.Count - 1;
            var mySeatId = 0;

            for (var i = 0; i < sortedSeatIds.Count; i++)
            {
                if (i != last)
                {
                    var currentSeatId = sortedSeatIds[i];
                    var nextSeatId = sortedSeatIds[i + 1];

                    if (nextSeatId - currentSeatId == _seatIdGap)
                    {
                        mySeatId = currentSeatId + 1;
                        break;
                    }
                }
            }

            return mySeatId.ToString();
        }

        private int GetSeatId(string code)
        {
            var row = GetRowNumber(code);
            var col = GetColumnNumber(code);

            row *= _seatIdMultiple;

            return row + col;
        }

        private int GetRowNumber(string code)
        {
            return GetPosition(code.Substring(_rowSubString.Start, _rowSubString.Length), _minRow, _maxRow, _frontChar);
        }

        private int GetColumnNumber(string code)
        {
            return GetPosition(code.Substring(_columnSubString.Start, _columnSubString.Length), _minCol, _maxCol, _leftChar);
        }

        private int GetPosition(string code, int lower, int upper, char upperChar)
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