using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using TextCopy;

namespace AdventOfCode
{
    public class Program
    {
        private const string _namespaceTemplate = "AdventOfCode.Year{0}.Day{1}.Task{2}";
        private const string _puzzleNameTemplate = "Advent Of Code {0} || Day {1}, Puzzle {2}";
        private const int _minYear = /*2015*/2020; // Started in 2015, but until I add them, this is limited.
        private const int _maxYear = 2020;
        private const int _minDay = 1;
        private const int _maxDay = 25;
        private const int _minPuzzle = 1;
        private const int _maxPuzzle = 2;
        private const int _horizontalRuleLength = 50;

        private static readonly Assembly _thisAssembly = typeof(Program).Assembly;

        private static async Task Main(string[] args)
        {
            var puzzles = new Dictionary<string, string>();

            Console.WriteLine(@"  __   ____  _  _  ____  __ _  ____     __  ____  ");
            Console.WriteLine(@" / _\ (    \/ )( \(  __)(  ( \(_  _)   /  \(  __) ");
            Console.WriteLine(@"/    \ ) D (\ \/ / ) _) /    /  )(    (  O )) _)  ");
            Console.WriteLine(@"\_/\_/(____/ \__/ (____)\_)__) (__)    \__/(__)   ");
            Console.WriteLine(@"  ___  __  ____  ____  _    ____   __  ____   __  ");
            Console.WriteLine(@" / __)/  \(    \(  __)(_)  (___ \ /  \(___ \ /  \ ");
            Console.WriteLine(@"( (__(  O )) D ( ) _)  _    / __/(  0 )/ __/(  0 )");
            Console.WriteLine(@" \___)\__/(____/(____)(_)  (____) \__/(____) \__/ ");
            Console.WriteLine(@"                                                  ");

            Console.WriteLine(" Welcome! Please choose an option:");
            Console.WriteLine();
            Console.WriteLine(" [0]  Enter Date & Puzzle (default)");
            Console.WriteLine(" [1]  Run Today, Puzzle 1");
            Console.WriteLine(" [2]  Run Today, Puzzle 2");
            Console.WriteLine(" [3]  Run All");


            Console.WriteLine();
            Console.Write(" Selection: ");
            var option = Console.ReadKey();

            Console.WriteLine();

            string dayString;
            int year;
            var currentDate = DateTime.Now;

            switch (option.KeyChar)
            {
                case '1':
                    var puzzle1 = 1;

                    year = currentDate.Year;
                    dayString = (currentDate.Day < 10) ? $"0{currentDate.Day}" : currentDate.Day.ToString();

                    puzzles.Add(string.Format(_namespaceTemplate, year, dayString, puzzle1), string.Format(_puzzleNameTemplate, year, dayString, puzzle1));
                    break;
                case '2':
                    var puzzle2 = 2;

                    year = currentDate.Year;
                    dayString = (currentDate.Day < 10) ? $"0{currentDate.Day}" : currentDate.Day.ToString();

                    puzzles.Add(string.Format(_namespaceTemplate, year, dayString, puzzle2), string.Format(_puzzleNameTemplate, year, dayString, puzzle2));
                    break;
                case '3':
                    for (var d = _minDay; d <= _maxDay; d++)
                    {
                        for (var y = _minYear; y <= _maxYear; y++)
                        {
                            for (var p = _minPuzzle; p <= _maxPuzzle; p++)
                            {
                                dayString = (d < 10) ? $"0{d}" : d.ToString();

                                puzzles.Add(string.Format(_namespaceTemplate, y, dayString, p), string.Format(_puzzleNameTemplate, y, dayString, p));
                            }
                        }
                    }

                    break;
                case '0':
                default:
                    int day;

                    Console.Write(" Year: ");
                    var yearInput = Console.ReadLine();

                    if (!int.TryParse(yearInput, out year))
                    {
                        throw new ArgumentException($" Input year is invalid. Entered: {yearInput}");
                    }

                    if (year < _minYear || year > 2020)
                    {
                        throw new ArgumentOutOfRangeException($" Input year is invalid. Must be between {_minYear} and {_maxYear} (inclusive). Entered: {year}");
                    }

                    Console.Write(" Day: ");
                    var dayInput = Console.ReadLine();

                    if (!int.TryParse(dayInput, out day))
                    {
                        throw new ArgumentException($" Input day is invalid. Entered: {dayInput}");
                    }

                    if (day < 1 || day > 25)
                    {
                        throw new ArgumentOutOfRangeException($" Input day is invalid. Must be between {_minDay} and {_maxDay} (inclusive). Entered: {day}");
                    }

                    dayString = (day < 10) ? $"0{day}" : day.ToString();

                    Console.Write(" Puzzle: ");
                    var puzzleInput = Console.ReadLine();

                    if (!int.TryParse(puzzleInput, out var puzzle))
                    {
                        throw new ArgumentException($" Input puzzle number is invalid. Entered: {puzzleInput}");
                    }

                    if (puzzle != 1 && puzzle != 2)
                    {
                        throw new ArgumentOutOfRangeException($" Input puzzle number is invalid. Must be either {_minPuzzle} or {_maxPuzzle}. Entered: {puzzle}");
                    }

                    puzzles.Add(string.Format(_namespaceTemplate, year, dayString, puzzle), string.Format(_puzzleNameTemplate, year, dayString, puzzle));
                    break;
            }

            var copyToClipboard = puzzles.Count == 1;

            foreach (var pzl in puzzles)
            {
                await ExecuteAsync(pzl.Key, pzl.Value, copyToClipboard).ConfigureAwait(false);
            }

            Console.WriteLine();
            Console.WriteLine(" Would you like to run another puzzle (y/n)?");
            Console.Write(" Selection: ");
            var runAgain = Console.ReadKey();

            switch (runAgain.KeyChar)
            {
                case 'y':
                case 'Y':
                    Console.WriteLine();
                    Console.WriteLine(" Press any key to reload.");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine();

                    // Starts a new instance of the program itself
                    Process.Start(AppDomain.CurrentDomain.FriendlyName);

                    // Closes the current process
                    Environment.Exit(0);

                    break;
                case 'n':
                case 'N':
                default:
                    Console.WriteLine();
                    Console.WriteLine(" Press any key to exit.");
                    Console.ReadKey();

                    break;
            }
        }

        private static async Task ExecuteAsync(string namespaceString, string puzzleName, bool copyToClipboard = false)
        {
            try
            {
                var type = _thisAssembly.GetType($"{namespaceString}");

                if (type != null)
                {
                    if (Activator.CreateInstance(type, null) is IRunnableCode runnableCode)
                    {
                        Console.WriteLine();
                        Console.WriteLine($" {new string('-', _horizontalRuleLength)}");
                        Console.WriteLine($" Running {puzzleName}");
                        Console.WriteLine($" {new string('-', _horizontalRuleLength)}");
                        Console.WriteLine();

                        var asyncTask = (Task)type.GetMethod("ExecuteAsync").Invoke(runnableCode, null);

                        await asyncTask.ConfigureAwait(false);

                        var resultProperty = asyncTask.GetType().GetProperty("Result");
                        var result = resultProperty.GetValue(asyncTask)?.ToString();

                        if (copyToClipboard)
                        {
                            await ClipboardService.SetTextAsync(result).ConfigureAwait(false);
                            Console.WriteLine($" {result} (copied to clipboard)");
                        }
                        else
                        {
                            Console.WriteLine($" {result}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{puzzleName} does not implement {nameof(IRunnableCode)}. Ensure namespace {namespaceString} implements {nameof(IRunnableCode)} in the codebase and retry.");
                    }
                }
                else
                {
                    Console.WriteLine($"{puzzleName} does not exist. Ensure namespace {namespaceString} exists in the codebase and retry.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($" {new string('*', _horizontalRuleLength)}");
                Console.WriteLine(" ERROR");
                Console.WriteLine($" {new string('*', _horizontalRuleLength)}");
                Console.WriteLine($" {ex.GetBaseException().Message}");
            }
        }
    }
}