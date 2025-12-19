using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal struct Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Cong vi tri vector(dx, dy)
        public Position Add (int dx, int dy)
        {
            return new Position(X + dx, Y + dy);
        }
        public static Position operator +(Position pos, (int dx, int dy) vector)
        {
            return new Position(pos.X + vector.dx, pos.Y + vector.dy);
        }

        //so sanh bang nhau
        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator == (Position left, Position right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
