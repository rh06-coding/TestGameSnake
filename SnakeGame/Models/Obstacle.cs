using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal class Obstacle
    {
        private static readonly Random _rng = new Random();
        public List<Position> Positions { get; private set; }

        public Obstacle()
        {
            Positions = new List<Position>();
        }

        // T?o ch??ng ng?i v?t ng?u nhiên trên map
        public void GenerateObstacles(int columns, int rows, IReadOnlyCollection<Position> occupied, int obstacleCount = 5)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            Positions.Clear();
            var blocked = new HashSet<Position>(occupied ?? new List<Position>());

            int attempts = 0;
            const int maxAttempts = 1000;

            while (Positions.Count < obstacleCount && attempts < maxAttempts)
            {
                attempts++;

                // Tránh biên ?? ??m b?o không gian ch?i
                int x = _rng.Next(2, columns - 2);
                int y = _rng.Next(2, rows - 2);
                var candidate = new Position(x, y);

                if (!blocked.Contains(candidate) && !Positions.Contains(candidate))
                {
                    Positions.Add(candidate);
                    blocked.Add(candidate);
                }
            }
        }

        // T?o ch??ng ng?i v?t theo m?u ??nh s?n
        public void GeneratePatternObstacles(int columns, int rows, int patternType = 1)
        {
            Positions.Clear();

            switch (patternType)
            {
                case 1:
                    GenerateRectanglePattern(columns, rows);
                    break;

                case 2:
                    GenerateCrossPattern(columns, rows);
                    break;

                case 3:
                    GenerateColumnsPattern(columns, rows);
                    break;

                case 4:
                    GenerateZigZagPattern(columns, rows);
                    break;

                default:
                    GenerateRandomPattern(columns, rows);
                    break;
            }
        }

        private void GenerateRectanglePattern(int columns, int rows)
        {
            int centerX = columns / 2;
            int centerY = rows / 2;
            int width = Math.Min(6, columns / 3);
            int height = Math.Min(4, rows / 3);

            for (int x = centerX - width / 2; x <= centerX + width / 2; x++)
            {
                Positions.Add(new Position(x, centerY - height / 2));
                Positions.Add(new Position(x, centerY + height / 2));
            }

            for (int y = centerY - height / 2; y <= centerY + height / 2; y++)
            {
                Positions.Add(new Position(centerX - width / 2, y));
                Positions.Add(new Position(centerX + width / 2, y));
            }
        }

        private void GenerateCrossPattern(int columns, int rows)
        {
            int centerX = columns / 2;
            int centerY = rows / 2;
            int length = Math.Min(8, Math.Min(columns, rows) / 3);

            for (int x = centerX - length; x <= centerX + length; x++)
            {
                if (x >= 1 && x < columns - 1)
                    Positions.Add(new Position(x, centerY));
            }

            for (int y = centerY - length; y <= centerY + length; y++)
            {
                if (y >= 1 && y < rows - 1)
                    Positions.Add(new Position(centerX, y));
            }
        }

        private void GenerateColumnsPattern(int columns, int rows)
        {
            int columnCount = 3;
            int spacing = columns / (columnCount + 1);

            for (int i = 1; i <= columnCount; i++)
            {
                int x = spacing * i;
                int columnHeight = rows / 2;
                int startY = (rows - columnHeight) / 2;

                for (int y = startY; y < startY + columnHeight; y++)
                {
                    if (x >= 1 && x < columns - 1 && y >= 1 && y < rows - 1)
                        Positions.Add(new Position(x, y));
                }
            }
        }

        private void GenerateZigZagPattern(int columns, int rows)
        {
            int startX = columns / 4;
            int endX = 3 * columns / 4;
            int midY = rows / 2;

            for (int x = startX; x <= endX; x++)
            {
                if (x >= 1 && x < columns - 1)
                    Positions.Add(new Position(x, midY - 3));
            }

            for (int x = startX; x <= endX; x++)
            {
                if (x >= 1 && x < columns - 1)
                    Positions.Add(new Position(x, midY + 3));
            }
        }

        private void GenerateRandomPattern(int columns, int rows)
        {
            int obstacleCount = (columns * rows) / 20;
            var occupied = new List<Position>();
            GenerateObstacles(columns, rows, occupied, obstacleCount);
        }

        // Ki?m tra va ch?m v?i v? trí
        public bool IsAt(Position position)
        {
            return Positions.Contains(position);
        }

        // Xóa t?t c? ch??ng ng?i v?t
        public void Clear()
        {
            Positions.Clear();
        }

        // Thêm ch??ng ng?i v?t tùy ch?nh
        public void AddObstacle(Position position)
        {
            if (!Positions.Contains(position))
            {
                Positions.Add(position);
            }
        }

        // Xóa ch??ng ng?i v?t t?i v? trí
        public void RemoveObstacle(Position position)
        {
            Positions.Remove(position);
        }
    }
}
