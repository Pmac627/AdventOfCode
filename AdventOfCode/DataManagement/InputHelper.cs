using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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
    }
}