using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static AdventOfCode.DTO.Enums;

namespace AdventOfCode.DataManagement
{
    public static class InputHelper
    {
        private const string _binPathMarker = "bin\\";
        private const string _fileDirectoryPathTemplate = @"{0}\Year{1}\Day{2}\Data";
        private const string _filePathTemplate = @"{0}\input.txt";

        public static async Task<string[]> GetPuzzleData(int year, string day)
        {
            var dataPath = string.Format(_filePathTemplate, GetPuzzleDataFolderPath(year, day));

            if (File.Exists(dataPath))
            {
                return await File.ReadAllLinesAsync(dataPath).ConfigureAwait(false);
            }

            return Array.Empty<string>();
        }

        public static string GetPuzzleDataFolderPath(int year, string day)
        {
            var assemblyLocation = Assembly.GetEntryAssembly().Location;
            var binPathIndex = assemblyLocation.IndexOf(_binPathMarker);
            var directoryPath = assemblyLocation.Substring(0, binPathIndex);

            return string.Format(_fileDirectoryPathTemplate, Path.GetDirectoryName(directoryPath), year, day);
        }

        public static List<T> ToNumberList<T>(this string[] data, SortRule sortDirection = SortRule.NoSort)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentNullException(nameof(data));
            }

            return typeof(T).Name switch
            {
                "Int16" => data.Select(x => short.Parse(x)).Sort(sortDirection) as List<T>,
                "Int32" => data.Select(x => int.Parse(x)).Sort(sortDirection) as List<T>,
                "Int64" => data.Select(x => long.Parse(x)).Sort(sortDirection) as List<T>,
                "Decimal" => data.Select(x => decimal.Parse(x)).Sort(sortDirection) as List<T>,
                "Single" => data.Select(x => float.Parse(x)).Sort(sortDirection) as List<T>,
                _ => throw new ArgumentException($"A non-numeric type was attempted to be converted. Requested Type: {typeof(T).Name}. Accepted types: short, int, long, decimal and float.", nameof(data)),
            };
        }

        public static List<T> Sort<T>(this IEnumerable<T> data, SortRule sortDirection = SortRule.NoSort, IComparer<T> comparer = null)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataToSort = data.ToList();

            switch (sortDirection)
            {
                case SortRule.OrderByAscending:
                    dataToSort = dataToSort.OrderBy(x => x).ToList();
                    break;
                case SortRule.OrderByDescending:
                    dataToSort = dataToSort.OrderByDescending(x => x).ToList();
                    break;
                case SortRule.DefaultComparer:
                    dataToSort.Sort();
                    break;
                case SortRule.CustomComparer:
                    dataToSort.Sort(comparer);
                    break;
                case SortRule.NoSort:
                default:
                    break;
            }

            return dataToSort;
        }
    }
}