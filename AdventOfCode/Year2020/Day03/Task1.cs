using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day03
{
    [ExpectedResult("171")]
	public class Task1 : IRunnableCode
	{
		private const char _treeChar = '#';

		public async Task<string> ExecuteAsync(string[] data)
		{
			var maxIndex = data[0].Length - 1;

			var totalTrees = CalculatePath(3, 1, maxIndex, data);

			return totalTrees.ToString();
		}

		private static int CalculatePath(int right, int down, int maxLength, string[] data)
		{
			var totalTrees = 0;
			var rightIndex = 0;

			for (var i = 0; i < data.Length; i += down)
			{
				var lane = data[i];

				if (lane[rightIndex] == _treeChar)
				{
					++totalTrees;
				}

				rightIndex += right;

				if (rightIndex > maxLength)
				{
					rightIndex %= lane.Length;
				}
			}

			return totalTrees;
		}
    }
}