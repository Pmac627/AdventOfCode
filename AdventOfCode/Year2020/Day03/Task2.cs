using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day03
{
    [ExpectedResult("1206576000")]
	public class Task2 : IRunnableCode
	{
		private const char _treeChar = '#';
		private static readonly (int Right, int Down) _firstPath = (1, 1);
		private static readonly (int Right, int Down) _secondPath = (3, 1);
		private static readonly (int Right, int Down) _thirdPath = (5, 1);
		private static readonly (int Right, int Down) _fourthPath = (7, 1);
		private static readonly (int Right, int Down) _fifthPath = (1, 2);

		public async Task<string> ExecuteAsync(string[] data)
		{
			var maxIndex = data[0].Length - 1;

			var totalTrees = CalculatePath(_firstPath.Right, _firstPath.Down, maxIndex, data);
			totalTrees *= CalculatePath(_secondPath.Right, _secondPath.Down, maxIndex, data);
			totalTrees *= CalculatePath(_thirdPath.Right, _thirdPath.Down, maxIndex, data);
			totalTrees *= CalculatePath(_fourthPath.Right, _fourthPath.Down, maxIndex, data);
			totalTrees *= CalculatePath(_fifthPath.Right, _fifthPath.Down, maxIndex, data);

			return totalTrees.ToString();
		}

		private static int CalculatePath(int right, int down, int maxLength, string[] data)
		{
			var totalTrees = 0;
			var rightIndex = 0;

			for (var i = 0; i < data.Length; i += down)
            {
				var lane = data[i].ToCharArray();

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