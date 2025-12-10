using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal struct Position : IEquatable<Position>
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Cong vi tri vector(dx, dy)
        public Position Add(int dx, int dy)
        {
            return new Position(X + dx, Y + dy);
        }

        public static Position operator +(Position pos, (int dx, int dy) vector)
        {
            return new Position(pos.X + vector.dx, pos.Y + vector.dy);
        }

        // So sanh bang nhau - tối ưu
        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        // GetHashCode tối ưu cho HashSet - sử dụng thuật toán tốt hơn
        public override int GetHashCode()
        {
            unchecked
            {
                // Sử dụng prime numbers để giảm collision
                int hash = 17;
                hash = hash * 31 + X;
                hash = hash * 31 + Y;
                return hash;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
