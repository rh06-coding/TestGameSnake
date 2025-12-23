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
        public Obstacle ObstacleRock { get; private set; }
        public Obstacle ObstacleSand { get; private set; }

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        public List<Particle> Particles { get; private set; }
        private Random _rng = new Random();

        // Cache HashSet cho collision detection nhanh hơn
        private HashSet<Position> _bodyPositions;
        private HashSet<Position> _obstacleRockPositions;
        private HashSet<Position> _obstacleSandPositions;

        public GameState(int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            Columns = columns;
            Rows = rows;
            _bodyPositions = new HashSet<Position>();
            _obstacleRockPositions = new HashSet<Position>();
            _obstacleSandPositions = new HashSet<Position>();

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
            ObstacleRock = new Obstacle();
            ObstacleSand = new Obstacle();
            
            Food = new Food();
            Particles = new List<Particle>();

            SpawnFood();
            
            // Update cache
            UpdateBodyPositionsCache();
            UpdateObstaclePositionsCache();
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
            if (_obstacleRockPositions.Contains(nextHead))
            {
                IsGameOver = true;
                return false;
            }
            if (_obstacleSandPositions.Contains(nextHead))
            {
                IsGameOver = true;
                return false;
            }

            // Kiểm tra va chạm thân - Kiểm tra với body hiện tại(chưa di chuyển)
            // Bỏ qua đuôi vì khi di chuyển đuôi sẽ bị xóa (trừ khi ăn)
            for (int i = 0; i < Snake.Body.Count - 1; i++)
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

                SpawnFoodParticles(Food.Location);
                Snake.Grow();
                Score += 10;
                SpawnFood();
            }

            UpdateBodyPositionsCache(); // Update cache sau khi grow

            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particles[i].Update();
                if (Particles[i].IsDead)
                    Particles.RemoveAt(i);
            }

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

        public void BuildMap(int mapType)
        {
            ObstacleRock.Clear();
            ObstacleSand.Clear();

            switch (mapType)
            {
                case 1:
                    Map1();
                    break;

                case 2:
                    Map2();
                    break;

                default:
                    Map1();
                    break;
            }

            UpdateObstaclePositionsCache();
            if (ObstacleRock.IsAt(Food.Location))
            {
                SpawnFood();
            }
            if (ObstacleSand.IsAt(Food.Location))
            {
                SpawnFood();
            }
        }


        private void BuildWallByRatio(double h1, double w1, double h2, double w2, Obstacle obstacle)
        {
            int r1 = (int)(h1 * Rows);
            int c1 = (int)(w1 * Columns);
            int r2 = (int)(h2 * Rows);
            int c2 = (int)(w2 * Columns);

            // ngang
            if (r1 == r2)
            {
                int start = Math.Min(c1, c2);
                int end = Math.Max(c1, c2);

                for (int c = start; c <= end; c++)
                    obstacle.AddObstacle(new Position(c, r1));
            }
            // dọc
            else if (c1 == c2)
            {
                int start = Math.Min(r1, r2);
                int end = Math.Max(r1, r2);

                for (int r = start; r <= end; r++)
                    obstacle.AddObstacle(new Position(c1, r));
            }
        }

        private void Map1()
        {
            BuildWallByRatio(1.0 / 5, 0.5 / 5, 1.0 / 5, 2.5 / 5, ObstacleRock);
            BuildWallByRatio(1.0 / 5, 0.5 / 5, 2.0 / 5, 0.5 / 5, ObstacleRock);

            BuildWallByRatio(4.0 / 5, 2.5 / 5, 4.0 / 5, 4.5 / 5, ObstacleRock);
            BuildWallByRatio(3.0 / 5, 4.5 / 5, 4.0 / 5, 4.5 / 5, ObstacleRock);
        }

        private void Map2()
        {
            BuildWallByRatio(0.5 / 5, 1.0 / 5, 2.5 / 5, 1.0 / 5, ObstacleRock);
            BuildWallByRatio(1.0 / 5, 0.5 / 5, 1.0 / 5, 2.5 / 5, ObstacleRock);

            BuildWallByRatio(3.0 / 5, 4.0 / 5, 4.0 / 5, 4.0 / 5, ObstacleRock);
            BuildWallByRatio(4.0 / 5, 3.0 / 5, 4.0 / 5, 4.0 / 5, ObstacleRock);

            BuildWallByRatio(0.5 / 5, 3.5 / 5, 0.5 / 5, 4.5 / 5, ObstacleSand);
            BuildWallByRatio(0.5 / 5, 4.5 / 5, 1.0 / 5, 4.5 / 5, ObstacleSand);

            BuildWallByRatio(3.5 / 5, 0.5 / 5, 4.5 / 5, 0.5 / 5, ObstacleSand);
            BuildWallByRatio(4.5 / 5, 0.5 / 5, 4.5 / 5, 1.0 / 5, ObstacleSand);
        }

        // Update cache cho chướng ngại vật
        private void UpdateObstaclePositionsCache()
        {
            _obstacleRockPositions.Clear();
            _obstacleSandPositions.Clear();
            if (ObstacleRock != null && ObstacleRock.Positions != null)
            {
                foreach (var pos in ObstacleRock.Positions)
                {
                    _obstacleRockPositions.Add(pos);
                }
            }
            if (ObstacleSand != null && ObstacleSand.Positions != null)
            {
                foreach (var pos in ObstacleSand.Positions)
                {
                    _obstacleSandPositions.Add(pos);
                }
            }
        }

        private void SpawnFood()
        {
            // Tạo danh sách các vị trí đã bị chiếm
            var occupied = new List<Position>(Snake.Body);
            if (ObstacleRock != null && ObstacleRock.Positions != null)
            {
                occupied.AddRange(ObstacleRock.Positions);
            }
            if (ObstacleSand != null && ObstacleSand.Positions != null)
            {
                occupied.AddRange(ObstacleSand.Positions);
            }

            Food.PlaceAtRandom(Columns, Rows, occupied);
        }

        private void SpawnFoodParticles(Position pos)
        {
            for (int i = 0; i < 3; i++)
            {
                Particles.Add(new Particle(pos, _rng));
            }
        }

    }
}
