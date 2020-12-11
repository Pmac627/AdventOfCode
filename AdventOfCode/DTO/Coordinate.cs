using System;

namespace AdventOfCode.DTO
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }


        public Coordinate CombineToNew(Coordinate second)
        {
            var x = X + second.X;
            var y = Y + second.Y;

            return new Coordinate(x, y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate externalID)
            {
                return Equals(externalID);
            }

            return false;
        }

        public bool Equals(Coordinate other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            return (X + Y).GetHashCode();
        }
    }
}