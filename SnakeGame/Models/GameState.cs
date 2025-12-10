using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal class GameState
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Snake Snake { get; private set; }
        public Food Food { get; private set; }

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        // Cache HashSet cho collision detection nhanh hơn
        private HashSet<Position> _bodyPositions;

        public GameState(int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            Columns = columns;
            Rows = rows;
            _bodyPositions = new HashSet<Position>();

            Reset();
        }

        public void Reset()
        {
            IsGameOver = false;
            Score = 0;
            var start = new Position(Columns / 2, Rows / 2);
            Snake = new Snake(start, Direction.Huong.Right);
            Snake.ClearDirectionQueue(); // Clear input queue khi reset
            Food = new Food();
            SpawnFood();
            
            // Update cache
            UpdateBodyPositionsCache();
        }

        public void ChangeDirection(Direction.Huong newDirection)
        {
            Snake.ChangeDirection(newDirection);
        }

        // Tick - tối ưu hóa collision detection
        public bool Tick()
        {
            if (IsGameOver) return false;

            // Di chuyen ran phia truoc
            Snake.Move();
            var head = Snake.Body[0];

            // Kiem tra va cham tuong (inline check nhanh hơn)
            if (head.X < 0 || head.X >= Columns || head.Y < 0 || head.Y >= Rows)
            {
                IsGameOver = true;
                return false;
            }

            // Kiem tra va cham than - sử dụng HashSet thay vì LINQ (O(1) vs O(n))
            // Chỉ kiểm tra từ phần tử thứ 1 trở đi (bỏ qua đầu)
            UpdateBodyPositionsCache();
            _bodyPositions.Remove(head); // Remove head trước khi check
            
            if (_bodyPositions.Count > 0 && _bodyPositions.Contains(head))
            {
                IsGameOver = true;
                return false;
            }

            // Kiem tra an thuc an
            if (Food.IsAt(head))
            {
                Snake.Grow();
                Score += 10;
                SpawnFood();
                UpdateBodyPositionsCache(); // Update cache sau khi grow
            }

            return true;
        }

        // Update cache positions để collision detection nhanh hơn
        private void UpdateBodyPositionsCache()
        {
            _bodyPositions.Clear();
            // Bỏ qua head (index 0) để kiểm tra self-collision
            for (int i = 1; i < Snake.Body.Count; i++)
            {
                _bodyPositions.Add(Snake.Body[i]);
            }
        }

        private void SpawnFood()
        {
            Food.PlaceAtRandom(Columns, Rows, Snake.Body);
        }
    }
}
