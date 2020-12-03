﻿using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day03
{
    public class Task1 : IRunnableCode
    {
        public async Task<string> ExecuteAsync()
		{
			var maxIndex = _data[0].Length - 1;

			var totalTrees = CalculatePath(3, 1, maxIndex);

			return totalTrees.ToString();
		}

		private int CalculatePath(int right, int down, int maxLength)
		{
			var totalTrees = 0;
			var rightIndex = 0;

			for (var i = 0; i < _data.Count; i += down)
			{
				var lane = _data[i].ToCharArray();

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

		private static IList<string> _data = new List<string>
        {
			"............#....#.............",
			"...........##....#......#..#..#",
			"......#.......#......#.........",
			"..#.#....#....#.............##.",
			"..#........####....#...#.......",
			"..##.....#.#.#..#.........#....",
			"...#.#..#..#....#..#..#........",
			"#.......#.........#....##.###..",
			"......##..#.#...#.......#.#....",
			"................##.........#.##",
			"..##..........#...#.........#.#",
			"..........#...##...............",
			"#...#......#..#.#..#...##..#...",
			"..##....#.......#......#..#....",
			"....#......#......#....#.......",
			".........#.....#..#............",
			".#...#.#.........#........#....",
			"#..........####.....#..........",
			"......##.....#....#..#........#",
			"#......#......#...........#....",
			"....#.........#....#...#..#..#.",
			".#........#......#.#.....#.....",
			"..#.#.#..........#....#.......#",
			"......#.#........##....##....##",
			".....#.#..#...#................",
			"......#......##...............#",
			"..#..##.............#...##.....",
			"......##......##..#......#.....",
			"....#.............#..##.....##.",
			"........#...............##.....",
			"..#......#.##..#...#....#...#..",
			"#......#.......#.............#.",
			".....#....##..............#....",
			"#.#.........#....#..##....#....",
			".#...#...#....#.#............#.",
			"...#...#.#..##.##.......##.....",
			"......#..#....##..#.#..#..#....",
			".......##..#..#......#..#.....#",
			".##..#......#..........#....#..",
			".....#................#..#....#",
			"........#..#....#.......#....#.",
			"..#......#.......#......#....#.",
			"....#...#.##........##....#....",
			".....#........#...........#....",
			"...#....##..........#..#...#.#.",
			"...#.......#......#...##...#...",
			".#.....#........#........#.#..#",
			".#.........#..##.....#.......#.",
			"....#..#....#.......#......#...",
			".#.#...##..##................##",
			"......#.#...#.......#....#....#",
			"........#....#..#.....#......#.",
			".......#..........#......#.....",
			"...............................",
			"..#..#####..#..#..........#.#..",
			".....#....##................#.#",
			".................##............",
			".#...#...#..#...........#...##.",
			"..#..#.#...........#.....##....",
			".#.......#.....#..##..#.#....#.",
			"..........#.#......##...##.....",
			"........##..#......##...#......",
			"#......................#.......",
			"............#.....#....#.#...#.",
			"#......#..........##..#........",
			".........#.......#...#.#.......",
			"...........##....#........#....",
			"#........#.....#...#........##.",
			".#......##......#.##.......#..#",
			".....#......#.#......#.......#.",
			".....#.#.........#.............",
			"...........#..#....#.....#.#...",
			"...#............#...#..........",
			"..#..#...#.....................",
			"......#..#...#....#............",
			".#.#.#........#..#...#.........",
			"..........#........#..#........",
			"..............#...#....#.......",
			"..#....#....##.......#...#.##..",
			".#.........#...#......#........",
			"..#......#...#.........##.#...#",
			"...#.....#...#..#.............#",
			".##........#.#.#.............#.",
			"..#.............#..#.#...#....#",
			"#...#.........#......#......#..",
			".......##..#.#..........#...#..",
			".......#.............#..#.#....",
			".#..#....#.#...................",
			"....##...#..#....#..#..........",
			"....#.#............#...........",
			"###........##..#.#..#..........",
			".#.#.#.......#...........#..#.#",
			"..........##..#.............#..",
			".#...........#......#.#..#..##.",
			"...###......#.##........#.....#",
			"....#..#..#...#................",
			"...#.....#........#............",
			"....#...#...#..#..##.##.......#",
			"#.......#......#....#.......#..",
			"#.............#...#............",
			"##......#..#...#....##.#...#...",
			".##....................#....#..",
			"..#.....#....#.#....#......#...",
			".......#..#..#............#...#",
			".#.....#.......#..#..#..#......",
			"......##.......................",
			"#..#...#.#.#....#.....#..#.....",
			"...................#...#...#...",
			"........#....##..#....#........",
			"##......#.#......##.###........",
			".........#...##................",
			".......#...#...#.......##......",
			"....#.......#......#.........##",
			"....#....#.#..#.....#..........",
			"...........#.......#........#..",
			"..#.........###.#........#.....",
			".......#...........#.#.....##..",
			"..#...#..#..........#..........",
			"..........#.#....#.............",
			".##....#........##.............",
			".............#.#####........#.#",
			".................##...#........",
			"##...#.#.......##........#.....",
			".#...#...#..#..#....#....#.....",
			"..#...#........#..#............",
			"##...#.#........#......##.#..##",
			".##......#..............##.#..#",
			".........#...#............#...#",
			"....#..#....#...........#......",
			"........#..#....#...##...#.....",
			"..#..............#...#.#.....#.",
			".#.......#.#.....#..###.......#",
			"...................#.......#...",
			"........##.....#..#.......##...",
			".....#....................#...#",
			"...#.#....#............#.#.....",
			"#.......#.......#....#.........",
			"..#...............#............",
			"##...#...#...#..............#..",
			"...#..........#..#....##.......",
			"#............##.##......#.#.#..",
			".#...........#.........#....##.",
			"..##....##.#....#.#.#.##...##.#",
			"........#.#.#.............#....",
			".#...........#....##...#...#.#.",
			".##...#.................#......",
			"....#.#..#....................#",
			".##......#........#..#.........",
			"...#...............#...........",
			".#.#..##..##.#........#........",
			"...........#....#.#.#......#...",
			"...................#........#.#",
			"..#............#...#.#........#",
			"....#....#.#.##......#...#.....",
			"..................#............",
			"..........................#....",
			"........#......................",
			"......#.#...#.#..##......#.#.#.",
			".........#...#..#..............",
			"..#.......#..........##..#.....",
			".........#............#........",
			"......#..#..#...###....#....#..",
			"#..#..............##.###..##..#",
			".#..................#.....#...#",
			"........#........#........#....",
			".........#........#.##......#..",
			"..#.....#.#..###...#....#......",
			"..#................##....#.....",
			"..#.#....##.....#......##...#..",
			"...#.......#........##.........",
			"#........#...#.#..........##..#",
			"................#...#.#.....#..",
			".........#..#..#.#..#.#...#....",
			"##....#...##.........#.#...#.##",
			"....#..#.....##.....#.....##...",
			"................#............#.",
			"..#..#...#.....#......#.....##.",
			"....#.......#...#...#...#..#...",
			"....#..##....#.###.#...#..#....",
			"#..##.....#.....#.##..##...##.#",
			".............###..........#....",
			"..................#.....###....",
			"..........#....#...#......#....",
			"...#..##.......#......#.#...#..",
			"..#.......................##.#.",
			"..#..#..#....#......#...#...##.",
			"#.............#................",
			"..........#.#.#.........#.#....",
			".....##..#......##.#...........",
			".#.#.#.#....#.#...#.....#.#...#",
			"......#.....##..............##.",
			"#..#.......##..##..............",
			"#..#..#................###.....",
			".....#......#.........#........",
			"#...........#........#.#.......",
			"#........#.#...#....#....###..#",
			"###..#.#...........#.##.....#.#",
			"..#..........#..#............#.",
			"...#....#.......#..#.....###...",
			".#....#.##.#..###..............",
			".....#.##.##.......###.##...#.#",
			"..#..##.......###..............",
			".#.........###..#..............",
			"..................###.....#..#.",
			"#....#....#.........#.....#....",
			".........#.#..#....#.....#.....",
			"....##.......##.......#.#......",
			".....#...#.##.....#............",
			"....#.#.#.......#..............",
			".##..#.#..#.......##...........",
			"....#....##..#.....##.......#.#",
			".....##....#..#.#........#.....",
			"........#.#.#....#....##...#..#",
			"..#......#.#.#..#.##....#.#.#..",
			"..#...#........#..#..........#.",
			".........#...................#.",
			"........#.....##..#....#....#..",
			"#..............#..........#....",
			"#........#.#...........#.#.....",
			"..#......................#.#..#",
			".........#.#.....#.#..........#",
			"......#....#.#.##........#.....",
			".#....##......##..#...#.......#",
			"..#........#...#.##....#..#.#..",
			".......#.....#..........#.....#",
			".........#.#..#.........#....#.",
			"..........#.##.........##..#...",
			"......#.#..#.....#.#..........#",
			"......#.#.#..#..#.#............",
			"...##.#..#..............#....#.",
			"#..........#...................",
			".#....#..#.#.......#........#..",
			"...#...#......#....#......#....",
			"..#.#.......#.......#.......#.#",
			"...#.#...#........#.....#......",
			"#.......#..#...................",
			"#..#..#.............#..#..#..#.",
			"#.......................#....##",
			".#.........#....#....#.........",
			"...............#...#..#....#..#",
			"#.....#.#...#.#.....#..........",
			"....##.#..#...#.#....###...#.#.",
			".................#....#........",
			"####.......##...##.......#.##..",
			"#..#....#....##............#...",
			"..##......#..#........#........",
			"....#..#..........#......#...##",
			"..#.#.............#...........#",
			"#...............#...#.......#.#",
			"#..#.........#.##.#.......#...#",
			"......#.....#.............#...#",
			"......#.##.........##...#......",
			"..#......##.#........#.......#.",
			"#..#.........#.##..............",
			"..#....#...#...#..#.....#.#....",
			"................#.......#......",
			"#.....#..............##....#.##",
			"##.....#...#.#.....#..##...#...",
			"#.#............##..........#..#",
			"..#.##......#..#....#..........",
			"....##.#....#.......##.....#...",
			"......#.#....###...#...........",
			"..................#......#....#",
			"..............##...............",
			"......#..#....#.....#..........",
			".......#........#...#..........",
			"..#......#......##..#.##..#....",
			"..#.#...#...............#......",
			"....#.#.............#.#......#.",
			"....#.#.....#......#..#.......#",
			"........................#..#...",
			".................#...........#.",
			"#......#......#.#.#.....##.....",
			"..#....##...#.....##.#.....#..#",
			"....#.........#....#.##.#.#....",
			"..#....###.....................",
			".....#.#....#......#....##....#",
			"#.......#...#......##.......#..",
			"#....#.........##.....#........",
			"#.....#...........#..#.....#...",
			".................#.....#..##..#",
			"..#...#......####...##.........",
			"...............................",
			"#........#.....#...............",
			".#.........#....#.#......##....",
			"...#..........#.........#.#.#.#",
			"......##......#....###........#",
			".....................#.#.#.....",
			"......#..#..#.......#...#......",
			"...##.#.............#.#.......#",
			"..#.#...#..#....#.....#.....#..",
			"..#..#.....................#..#",
			"........#....#..........#..#...",
			"#.##....#..#.#..#............#.",
			"..............###.............#",
			".#.#..........#.#....#...#....#",
			"....#..........#.#..#......#...",
			".........##.#...#..............",
			"..................#.....#.#....",
			".#....#.......#.##.#.........#.",
			".##..#...#......#..#...........",
			".#.........#..........#.#......",
			"#.#......#.#.#.#.......#...#.#.",
			".......#....#.#......#......#..",
			"...#..#....#.#..#..##...##.....",
			"#.#.#.......#....#.........##..",
			"#..#....#........###....#.#....",
			"....#..#.........#....#...#....",
			"...#.#.#.#..#..##.....#.##.....",
			".......#.......#...............",
			"#.#.#......##....#.............",
			"...#.##........#.....#...##.#..",
			"...#.#.###..........#.......#..",
			".....#...#.......#.........#...",
			"............#..#...#..##.......",
			"...#....#..##.##..........#.##.",
			"..................#........#...",
			"....#.##.#.##........#.#.......",
			".#...........##.....##.......#.",
			"#...#.........#.....##.........",
			"#..#....#.#.........#..........",
			"..#......#.#.#......#.....#..#.",
			"..##......#..............#....."
		};
    }
}