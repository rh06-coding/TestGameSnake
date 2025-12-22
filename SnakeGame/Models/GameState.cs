using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SnakeGame.Services;

namespace SnakeGame.Models
{
    internal class GameState
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public Obstacle Obstacle { get; private set; }

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        // Cache HashSet cho collision detection nhanh hơn
        private HashSet<Position> _bodyPositions;
        private HashSet<Position> _obstaclePositions;

        public GameState(int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            Columns = columns;
            Rows = rows;
            _bodyPositions = new HashSet<Position>();
            _obstaclePositions = new HashSet<Position>();

            Reset();
        }

        public void Reset()
        {
            IsGameOver = false;
            Score = 0;
            var start = new Position(Columns / 2, Rows / 2);
            Snake = new Snake(start, Direction.Huong.Right);
            Snake.ClearDirectionQueue();
            SoundService.PlayInGame();
            
            // Khởi tạo chướng ngại vật
            Obstacle = new Obstacle();
            
            Food = new Food();

            GenerateObstacles(patternType: 0, obstacleCount: 15);

            SpawnFood();
            
            // Update cache
            UpdateBodyPositionsCache();
            UpdateObstaclePositionsCache();
        }

        // Phương thức để tạo chướng ngại vật (có thể gọi từ GameForm)
        public void GenerateObstacles(int patternType = 0, int obstacleCount = 5)
        {
            if (patternType == 0)
            {
                // Ngẫu nhiên
                var occupied = new List<Position>(Snake.Body);
                occupied.Add(Food.Location);
                Obstacle.GenerateObstacles(Columns, Rows, occupied, obstacleCount);
            }
            else
            {
                // Theo mẫu
                Obstacle.GeneratePatternObstacles(Columns, Rows, patternType);
            }
            
            UpdateObstaclePositionsCache();
            
            // Đảm bảo food không spawn trên obstacle
            if (Obstacle.IsAt(Food.Location))
            {
                SpawnFood();
            }
        }

        public void ChangeDirection(Direction.Huong newDirection)
        {
            Snake.ChangeDirection(newDirection);
        }

        // Tick - tối ưu hóa collision detection
        public bool Tick()
        {
            if (IsGameOver) return false;

            // Tính toán vị trí đầu rắn mới trươc khi di chuyển
            Position nextHead = GetNextHeadPosition();

            // Kiểm tra va chạm tường
            if (nextHead.X < 0 || nextHead.X >= Columns || nextHead.Y < 0 || nextHead.Y >= Rows)
            {
                IsGameOver = true;
                return false;
            }

            // Kiểm tra va chạm chướng ngại vật
            if (_obstaclePositions.Contains(nextHead))
            {
                IsGameOver = true;
                return false;
            }

            // Kiểm tra va chạm thân - Kiểm tra với body hiện tại(chưa di chuyển)
            // Bỏ qua đuôi vì khi di chuyển đuôi sẽ bị xóa (trừ khi ăn)
            for(int i = 0; i < Snake.Body.Count - 1; i++)
            {
                if (Snake.Body[i].Equals(nextHead))
                {
                    IsGameOver = true;
                    return false;
                }
            }

            // Kiểm tra thức ăn trước khi di chuyển
            bool eatFood = Food.IsAt(nextHead);

            // Nếu ăn thức ăn, kiểm tra đuôi
            if (eatFood && Snake.Body[Snake.Body.Count - 1] == nextHead)
            {
                IsGameOver = true;
                return false;
            }

            // Di chuyển rắn
            Snake.Move();

            // Kiểm tra ăn thức ăn
            if (eatFood)
            {
                SoundService.PlayEat();
                Snake.Grow();
                Score += 10;
                SpawnFood();
            }

            UpdateBodyPositionsCache(); // Update cache sau khi grow

            return true;
        }

        // Tính toán vị trí đầu rắn tiếp theo dựa trên hướng hiện tại
        private Position GetNextHeadPosition()
        {
            var head = Snake.Body[0];
            var direction = Snake.CurrentDirection;

            //Nếu có hướng trong queue thì lấy hướng đó
            if(Snake.PendingDirectionCount > 0)
            {
                //Cần lấy hướng tiếp theo từ queue để tính toán vị trí đầu rắn mới
                direction = Snake.GetNextDirection();
            }

            switch(direction)
            {
                case Direction.Huong.Up:
                    return head.Add(0, -1);
                case Direction.Huong.Down:
                    return head.Add(0, 1);
                case Direction.Huong.Left:
                    return head.Add(-1, 0);
                case Direction.Huong.Right:
                    return head.Add(1, 0);
                default:
                    return head;
            }
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

        // Update cache cho chướng ngại vật
        private void UpdateObstaclePositionsCache()
        {
            _obstaclePositions.Clear();
            if (Obstacle != null && Obstacle.Positions != null)
            {
                foreach (var pos in Obstacle.Positions)
                {
                    _obstaclePositions.Add(pos);
                }
            }
        }

        private void SpawnFood()
        {
            // Tạo danh sách các vị trí đã bị chiếm
            var occupied = new List<Position>(Snake.Body);
            if (Obstacle != null && Obstacle.Positions != null)
            {
                occupied.AddRange(Obstacle.Positions);
            }
            
            Food.PlaceAtRandom(Columns, Rows, occupied);
        }
    }
}
