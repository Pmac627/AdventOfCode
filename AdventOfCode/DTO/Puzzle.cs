namespace AdventOfCode.DTO
{
    public class Puzzle
    {
        private const string _namespaceTemplate = "AdventOfCode.Year{0}.Day{1}.Task{2}";
        private const string _puzzleNameTemplate = "Advent Of Code {0} || Day {1}, Puzzle {2}";
        private const string _singleDigitDayTemplate = "0{0}";

        public Puzzle(int year, int day, int puzzle)
        {
            Year = year;
            Day = day;
            DayString = (Day < 10) ? string.Format(_singleDigitDayTemplate, day) : Day.ToString();
            PuzzleNumber = puzzle;
            PuzzleName = string.Format(_puzzleNameTemplate, year, DayString, puzzle);
            PuzzleNameSpace = string.Format(_namespaceTemplate, year, DayString, puzzle);
        }

        public int Year { get; init; }
        public int Day { get; init; }
        public int PuzzleNumber { get; init; }
        public string PuzzleName { get; init; }
        public string PuzzleNameSpace { get; init; }
        public string DayString { get; init; }
    }
}