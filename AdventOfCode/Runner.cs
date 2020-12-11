using AdventOfCode.DataManagement;
using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace AdventOfCode
{
    public class Runner
    {
        private const int _minYear = /*2015*/2020; // Started in 2015, but until I add them, this is limited.
        private const int _maxYear = 2020;
        private const int _minDay = 1;
        private const int _maxDay = 25;
        private const int _minPuzzle = 1;
        private const int _maxPuzzle = 2;
        private const int _minMenuChoice = 0;
        private const int _maxMenuChoice = 6;
        private const int _horizontalRuleLength = 50;
        private const int _validCurrentMonth = 12;
        private const int _exitPauseInSeconds = 1;
        private const int _gracefulExitCode = 0;

        private const char _validInputZero = '0';
        private const char _validInputOne = '1';
        private const char _validInputTwo = '2';
        private const char _validInputThree = '3';
        private const char _validInputFour = '4';
        private const char _validInputFive = '5';
        private const char _validInputSix = '6';
        private const char _validNextStepLowerY = 'y';
        private const char _validNextStepUpperY = 'Y';
        private const char _validNextStepLowerN = 'n';
        private const char _validNextStepUpperN = 'N';
        private const char _repeatedCharHorizontalRule = '-';
        private const char _repeatedCharError = '*';

        private const ConsoleColor _defaultFontColor = ConsoleColor.Gray;
        private const ConsoleColor _errorFontColor = ConsoleColor.Red;
        private const ConsoleColor _inputFontColor = ConsoleColor.DarkGreen;
        private const ConsoleColor _resultFontColor = ConsoleColor.DarkCyan;
        private const ConsoleColor _expectedResultFontColor = ConsoleColor.Green;
        private const ConsoleColor _timerReadoutFontColor = ConsoleColor.DarkYellow;

        private static readonly Assembly _thisAssembly = typeof(Runner).Assembly;

        private static async Task Main(string[] _)
        {
            await Run().ConfigureAwait(false);
        }

        private static async Task Run(bool isRerun = false)
        {
            if (!isRerun)
            {
                GenerateLogo();
            }

            var option = GenerateMenu();

            var currentDate = DateTime.Now;

            ValidateRunTodayIsAllowed(currentDate, option.KeyChar);
            
            var puzzles = new List<Puzzle>();
            var puzzle1 = 1;
            var puzzle2 = 2;

            switch (option.KeyChar)
            {
                case _validInputOne:
                    puzzles.Add(new Puzzle(currentDate.Year, currentDate.Day, puzzle1));

                    break;
                case _validInputTwo:
                    puzzles.Add(new Puzzle(currentDate.Year, currentDate.Day, puzzle2));

                    break;
                case _validInputThree:
                    puzzles.Add(new Puzzle(currentDate.Year, currentDate.Day, puzzle1));
                    puzzles.Add(new Puzzle(currentDate.Year, currentDate.Day, puzzle2));

                    break;
                case _validInputFour:
                    puzzles.AddRange(CreateListOfSpecificDaysPuzzles(currentDate));

                    break;
                case _validInputFive:
                    puzzles.AddRange(CreateListOfAllPuzzles(currentDate));

                    break;
                case _validInputSix:
                    GenerateExitMessage();

                    break;
                case _validInputZero:
                default:
                    puzzles.Add(GenerateManualRunMenu());

                    break;
            }

            await ExecutePuzzleList(puzzles).ConfigureAwait(false);

            await GenerateRerunMenu().ConfigureAwait(false);
        }

        private static ConsoleKeyInfo GetKeyInput()
        {
            Console.ForegroundColor = _inputFontColor;
            var option = Console.ReadKey();
            Console.ForegroundColor = _defaultFontColor;

            return option;
        }

        private static string GetLineInput()
        {
            Console.ForegroundColor = _inputFontColor;
            var line = Console.ReadLine();
            Console.ForegroundColor = _defaultFontColor;

            return line;
        }

        private static void GenerateLogo()
        {
            Console.ForegroundColor = _resultFontColor;
            Console.WriteLine(@"  __   ____  _  _  ____  __ _  ____     __  ____  ");
            Console.WriteLine(@" / _\ (    \/ )( \(  __)(  ( \(_  _)   /  \(  __) ");
            Console.WriteLine(@"/    \ ) D (\ \/ / ) _) /    /  )(    (  O )) _)  ");
            Console.WriteLine(@"\_/\_/(____/ \__/ (____)\_)__) (__)    \__/(__)   ");
            Console.WriteLine(@"  ___  __  ____  ____  _    ____   __  ____   __  ");
            Console.WriteLine(@" / __)/  \(    \(  __)(_)  (___ \ /  \(___ \ /  \ ");
            Console.WriteLine(@"( (__(  O )) D ( ) _)  _    / __/(  0 )/ __/(  0 )");
            Console.WriteLine(@" \___)\__/(____/(____)(_)  (____) \__/(____) \__/ ");
            Console.WriteLine(@"                                                  ");
            Console.ForegroundColor = _defaultFontColor;
        }

        private static ConsoleKeyInfo GenerateMenu()
        {
            Console.WriteLine(" Welcome! Please choose an option:");
            Console.WriteLine();
            Console.WriteLine($" [{_validInputZero}]  Enter Date & Puzzle (default)");
            Console.WriteLine($" [{_validInputOne}]  Run Today, Puzzle 1");
            Console.WriteLine($" [{_validInputTwo}]  Run Today, Puzzle 2");
            Console.WriteLine($" [{_validInputThree}]  Run Today, Both Puzzles");
            Console.WriteLine($" [{_validInputFour}]  Run Specific Day's Puzzles");
            Console.WriteLine($" [{_validInputFive}]  Run All");
            Console.WriteLine($" [{_validInputSix}]  Exit");

            Console.WriteLine();
            Console.Write(" Selection: ");
            var option = GetKeyInput();

            Console.WriteLine();

            if (!char.IsNumber(option.KeyChar))
            {
                Console.WriteLine();
                Console.ForegroundColor = _errorFontColor;
                Console.WriteLine($" Only a number between {_minMenuChoice} and {_maxMenuChoice} is allowed. You entered: {option.KeyChar}. Please select again.");
                Console.ForegroundColor = _defaultFontColor;
                Console.WriteLine();

                return GenerateMenu();
            }

            return option;
        }

        private static void ValidateRunTodayIsAllowed(DateTime currentDate, char selectedOption)
        {
            if (new char[3] { _validInputOne, _validInputTwo, _validInputThree }.Contains(selectedOption) &&
                (
                    (currentDate.Month != _validCurrentMonth) ||
                    currentDate.Year < _minYear || currentDate.Year > _maxYear ||
                    currentDate.Day < _minDay || currentDate.Day > _maxDay
                )
            )
            {
                throw new ArgumentOutOfRangeException($" This option only works during the period of December {_minDay}st - {_maxDay}th of valid years.");
            }
        }

        private static IList<Puzzle> CreateListOfSpecificDaysPuzzles(DateTime currentDate)
        {
            var puzzles = new List<Puzzle>();

            Console.Write(" Day: ");
            var dayInput = GetLineInput();

            if (!int.TryParse(dayInput, out var day))
            {
                throw new ArgumentException($" Input day is invalid. Entered: {dayInput}.");
            }

            if (day < _minDay || day > _maxDay)
            {
                throw new ArgumentOutOfRangeException($" Input day is invalid. Must be between {_minDay} and {_maxDay} (inclusive). Entered: {day}.");
            }

            var year = currentDate.Year;

            if (year > _maxYear)
            {
                year = _maxYear;
            }

            for (var p = _minPuzzle; p <= _maxPuzzle; p++)
            {
                puzzles.Add(new Puzzle(year, day, p));
            }

            return puzzles;
        }

        private static IList<Puzzle> CreateListOfAllPuzzles(DateTime currentDate)
        {
            var puzzles = new List<Puzzle>();
            var maxDay = _maxDay;

            if (currentDate.Month == _validCurrentMonth)
            {
                if (currentDate.Day < _maxDay)
                {
                    maxDay = currentDate.Day;
                }
            }

            for (var d = _minDay; d <= maxDay; d++)
            {
                for (var y = _minYear; y <= _maxYear; y++)
                {
                    for (var p = _minPuzzle; p <= _maxPuzzle; p++)
                    {
                        puzzles.Add(new Puzzle(y, d, p));
                    }
                }
            }

            return puzzles;
        }

        private static Puzzle GenerateManualRunMenu()
        {
            Console.Write(" Year: ");
            var yearInput = GetLineInput();

            if (!int.TryParse(yearInput, out var year))
            {
                throw new ArgumentException($" Input year is invalid. Entered: {yearInput}.");
            }

            if (year < _minYear || year > _maxYear)
            {
                throw new ArgumentOutOfRangeException($" Input year is invalid. Must be between {_minYear} and {_maxYear} (inclusive). Entered: {year}.");
            }

            Console.Write(" Day: ");
            var dayInput = GetLineInput();

            if (!int.TryParse(dayInput, out var day))
            {
                throw new ArgumentException($" Input day is invalid. Entered: {dayInput}.");
            }

            if (day < _minDay || day > _maxDay)
            {
                throw new ArgumentOutOfRangeException($" Input day is invalid. Must be between {_minDay} and {_maxDay} (inclusive). Entered: {day}.");
            }

            Console.Write(" Puzzle: ");
            var puzzleInput = GetLineInput();

            if (!int.TryParse(puzzleInput, out var puzzle))
            {
                throw new ArgumentException($" Input puzzle number is invalid. Entered: {puzzleInput}.");
            }

            if (puzzle != _minPuzzle && puzzle != _maxPuzzle)
            {
                throw new ArgumentOutOfRangeException($" Input puzzle number is invalid. Must be either {_minPuzzle} or {_maxPuzzle}. Entered: {puzzle}.");
            }

            return new Puzzle(year, day, puzzle);
        }

        private static async Task ExecutePuzzleList(IList<Puzzle> puzzles)
        {
            var copyToClipboard = puzzles.Count == 1;

            foreach (var puzzle in puzzles)
            {
                var stopWatch = Stopwatch.StartNew();
                var data = await InputHelper.GetPuzzleData(puzzle.Year, puzzle.DayString).ConfigureAwait(false);
                stopWatch.Stop();
                var dataReadInMs = stopWatch.ElapsedMilliseconds;

                await ExecuteAsync(puzzle.PuzzleNameSpace, puzzle.PuzzleName, data, dataReadInMs, copyToClipboard).ConfigureAwait(false);
            }
        }

        private static async Task ExecuteAsync(string namespaceString, string puzzleName, string[] data, long dataReadInMs, bool copyToClipboard = false)
        {
            try
            {
                var type = _thisAssembly.GetType($"{namespaceString}");

                if (type != null)
                {
                    if (Activator.CreateInstance(type, null) is IRunnableCode runnableCode)
                    {
                        Console.WriteLine();
                        Console.WriteLine($" {new string(_repeatedCharHorizontalRule, _horizontalRuleLength)}");
                        Console.WriteLine($"  Running {puzzleName}");
                        Console.ForegroundColor = _timerReadoutFontColor;
                        Console.WriteLine($"  Data Readtime: {dataReadInMs}ms || Lines: {data.Length}");
                        Console.ForegroundColor = _defaultFontColor;
                        Console.WriteLine($" {new string(_repeatedCharHorizontalRule, _horizontalRuleLength)}");
                        Console.WriteLine();

                        if (data != null && data.Any())
                        {
                            var stopWatch = Stopwatch.StartNew();

                            var asyncTask = (Task)type.GetMethod("ExecuteAsync").Invoke(runnableCode, new object[1] { data });

                            await asyncTask.ConfigureAwait(false);

                            stopWatch.Stop();
                            var runTimeInMs = stopWatch.ElapsedMilliseconds;

                            var resultProperty = asyncTask.GetType().GetProperty("Result");
                            var result = resultProperty.GetValue(asyncTask)?.ToString();
                            var expectedValue = string.Empty;

                            if (Attribute.GetCustomAttribute(type, typeof(ExpectedResultAttribute)) is ExpectedResultAttribute expectedResultAttribute)
                            {
                                Console.ForegroundColor = _expectedResultFontColor;
                                Console.WriteLine($" Expected: {expectedResultAttribute.ExpectedValue}");

                                if (expectedResultAttribute.ExpectedValue != result)
                                {
                                    Console.ForegroundColor = _errorFontColor;
                                }
                                else
                                {
                                    Console.ForegroundColor = _resultFontColor;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = _resultFontColor;
                            }

                            if (copyToClipboard)
                            {
                                await ClipboardService.SetTextAsync(result).ConfigureAwait(false);

                                Console.WriteLine($" Returned: {result} (copied to clipboard)");
                            }
                            else
                            {
                                Console.WriteLine($" Returned: {result}");
                            }

                            Console.ForegroundColor = _timerReadoutFontColor;
                            Console.WriteLine($" Run time: {runTimeInMs}ms.");
                            Console.ForegroundColor = _defaultFontColor;
                        }
                        else
                        {
                            Console.ForegroundColor = _errorFontColor;
                            Console.WriteLine(" No data available. Skipping execution.");
                            Console.ForegroundColor = _defaultFontColor;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = _errorFontColor;
                        Console.WriteLine($" {puzzleName} does not implement {nameof(IRunnableCode)}. Ensure namespace {namespaceString} implements {nameof(IRunnableCode)} in the codebase and retry.");
                        Console.ForegroundColor = _defaultFontColor;
                    }
                }
                else
                {
                    Console.ForegroundColor = _errorFontColor;
                    Console.WriteLine($" {puzzleName} does not exist. Ensure namespace {namespaceString} exists in the codebase and retry.");
                    Console.ForegroundColor = _defaultFontColor;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = _errorFontColor;
                Console.WriteLine();
                Console.WriteLine($" {new string(_repeatedCharError, _horizontalRuleLength)}");
                Console.WriteLine(" ERROR");
                Console.WriteLine($" {new string(_repeatedCharError, _horizontalRuleLength)}");
                Console.WriteLine($" {ex.GetBaseException().Message}");
                Console.ForegroundColor = _defaultFontColor;
            }
        }

        private static Task GenerateRerunMenu()
        {
            Console.WriteLine();
            Console.WriteLine(" Would you like to run another puzzle (y/n)?");
            Console.WriteLine();
            Console.Write(" Selection: ");
            var runAgain = GetKeyInput();
            Console.WriteLine();

            switch (runAgain.KeyChar)
            {
                case _validNextStepLowerY:
                case _validNextStepUpperY:
                    Console.WriteLine();
                    Console.WriteLine(" Press any key to reload.");
                    GetKeyInput();

                    Console.WriteLine();
                    Console.WriteLine();

                    return Run(true);
                case _validNextStepLowerN:
                case _validNextStepUpperN:
                default:
                    GenerateExitMessage();

                    break;
            }

            return Task.CompletedTask;
        }

        private static void GenerateExitMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = _resultFontColor;
            Console.WriteLine(" Goodbye!");
            Console.ForegroundColor = _defaultFontColor;

            Thread.Sleep(TimeSpan.FromSeconds(_exitPauseInSeconds));
            Environment.Exit(_gracefulExitCode);
        }
    }
}