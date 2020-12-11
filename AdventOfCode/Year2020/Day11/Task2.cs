using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day11
{
    [ExpectedResult("2214")]
    public class Task2 : IRunnableCode
    {
        private const int _minSpaceNeeded = 5;

        private const char _emptySeat = 'L';
        private const char _floor = '.';
        private const char _occupiedSeat = '#';

        private static int _maxXCoordinate = 0;
        private static int _maxYCoordinate = 0;

        private static readonly IList<Coordinate> _relationalCoordinates = new List<Coordinate>()
        {
            new Coordinate(1, 0),
            new Coordinate(1, 1),
            new Coordinate(0, 1),
            new Coordinate(-1, 1),
            new Coordinate(-1, 0),
            new Coordinate(-1, -1),
            new Coordinate(0, -1),
            new Coordinate(1, -1)
        };

        private static IDictionary<Coordinate, bool> _seats = new Dictionary<Coordinate, bool>();

        public async Task<string> ExecuteAsync(string[] data)
        {
            var rows = data.ToList();
            _maxYCoordinate = rows.Count;

            for (var i = 0; i < rows.Count; i++)
            {
                for (var j = 0; j < rows[i].Length; j++)
                {
                    if (_maxXCoordinate == 0)
                    {
                        _maxXCoordinate = rows[i].Length - 1;
                    }

                    var seat = rows[i][j];

                    switch (seat)
                    {
                        case _emptySeat:
                            var emptyCoordinate = new Coordinate(j, i);

                            _seats[emptyCoordinate] = false;

                            break;
                        case _occupiedSeat:
                            var occupiedCoordinate = new Coordinate(j, i);

                            _seats[occupiedCoordinate] = false;

                            break;
                        case _floor:
                        default:
                            break;
                    }
                }
            }

            var seatedCount = CalculateSeating();

            return seatedCount.ToString();
        }

        private static int CalculateSeating()
        {
            var seatsChanged = 0;

            do
            {
                seatsChanged = 0;
                var nextSeats = new Dictionary<Coordinate, bool>(_seats);

                foreach (var seat in _seats)
                {
                    var nextSeat = ValidateRelativePosition(seat.Key);

                    if (nextSeat != seat.Value)
                    {
                        seatsChanged++;
                    }

                    nextSeats[seat.Key] = nextSeat;
                }

                _seats = new Dictionary<Coordinate, bool>(nextSeats);

            } while (seatsChanged != 0);

            return _seats.Count(x => x.Value);
        }

        private static bool ValidateRelativePosition(Coordinate coordinate)
        {
            var seatedPeople = 0;
            var nearbyPeople = new List<Coordinate>();

            foreach (var relation in _relationalCoordinates)
            {
                var occupiedSeat = coordinate.CombineToNew(relation);

                while (!_seats.ContainsKey(occupiedSeat))
                {
                    if (occupiedSeat.X < 0 || occupiedSeat.X > _maxXCoordinate || occupiedSeat.Y < 0 || occupiedSeat.Y > _maxYCoordinate)
                    {
                        break;
                    }

                    occupiedSeat = occupiedSeat.CombineToNew(relation);
                }

                nearbyPeople.Add(occupiedSeat);
            }

            foreach (var n in nearbyPeople)
            {
                if (!_seats.ContainsKey(n))
                {
                    continue;
                }

                if (_seats[n])
                {
                    seatedPeople++;
                }
            }

            if (_seats[coordinate])
            {
                if (seatedPeople < _minSpaceNeeded)
                {
                    return true;
                }
            }
            else
            {
                if (seatedPeople == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}