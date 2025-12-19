using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal class Snake
    {
        public List<Position> Body { get; private set; }
        public Direction.Huong CurrentDirection { get; private set; }
        private Direction.Huong _nextDirection;

        public Snake(Position startPos, Direction.Huong startDirec)
        {
            Body = new List<Position> { startPos };
            CurrentDirection = startDirec;
            _nextDirection = startDirec;
        }

        //doi huong di chuyen
        public void ChangeDirection(Direction.Huong newDirection)
        {
            if(!IsOppositeDirection(CurrentDirection, newDirection) && newDirection != CurrentDirection)
            {
                _nextDirection = newDirection;
            }
        }

        //Di chuyen ran
        public void Move()
        {
            CurrentDirection = _nextDirection;
            var vector = GetVector(CurrentDirection);
            var newHead = Body[0] + (vector.dx, vector.dy);
            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        //Tang chieu dai ran
        public void Grow()
        {
            Body.Add(Body[Body.Count - 1]);
        }

        //Lay vector di chuyen tu huong
        private (int dx, int dy) GetVector(Direction.Huong direction)
        {
            switch(direction)
            {
                case Direction.Huong.Up: return (0, -1);
                case Direction.Huong.Down: return (0, 1);
                case Direction.Huong.Left: return (-1, 0);
                case Direction.Huong.Right: return (1, 0);
                default: return (0, 0);
            }
        }

        //Kiem tra huong moi co phai la huong nguoc lai khong
        private bool IsOppositeDirection(Direction.Huong a, Direction.Huong b)
        {
            return (a == Direction.Huong.Up && b == Direction.Huong.Down) ||
                   (a == Direction.Huong.Down && b == Direction.Huong.Up) ||
                   (a == Direction.Huong.Left && b == Direction.Huong.Right) ||
                   (a == Direction.Huong.Right && b == Direction.Huong.Left);
        }
    }
}
