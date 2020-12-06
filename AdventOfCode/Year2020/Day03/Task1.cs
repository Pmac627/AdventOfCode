using AdventOfCode.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day03
{
    public class Task1 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
		{
			var maxIndex = data[0].Length - 1;

			var totalTrees = CalculatePath(3, 1, maxIndex, data);

			return totalTrees.ToString();
		}

		private int CalculatePath(int right, int down, int maxLength, string[] data)
		{
			var totalTrees = 0;
			var rightIndex = 0;

			for (var i = 0; i < data.Count(); i += down)
			{
				var lane = data[i].ToCharArray();

				if (lane[rightIndex] == '#')
				{
					++totalTrees;
				}

				rightIndex += right;

				if (rightIndex > maxLength)
				{
					rightIndex = rightIndex % lane.Length;
				}
			}

			return totalTrees;
		}
    }
}