using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode.DataManagement
{
    public static class InputHelper
    {
        private const string _fileName = "input.txt";

        public static async Task<string[]> GetPuzzleData(int year, string day)
        {
            var dataPath = $@"{GetPuzzleDataFolderPath(year, day)}\{_fileName}";

            if (File.Exists(dataPath))
            {
                return await File.ReadAllLinesAsync(dataPath).ConfigureAwait(false);
            }

            return Array.Empty<string>();
        }

        public static string GetPuzzleDataFolderPath(int year, string day)
        {
            var assemblyLocation = Assembly.GetEntryAssembly().Location;
            var rootDirPath = Path.GetDirectoryName(assemblyLocation.Substring(0, assemblyLocation.IndexOf("bin\\")));

            return $@"{rootDirPath}\Year{year}\Day{day}\Data";
        }
    }
}