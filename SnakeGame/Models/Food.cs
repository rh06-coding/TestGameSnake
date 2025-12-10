using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal class Food
    {
        private static readonly Random _rng = new Random();
        public Position Location { get; private set; }

        public Food()
        {
            Location = new Position(0, 0);
        }

        // Đặt thức ăn ở vị trí ngẫu nhiên không trùng với rắn
        public void PlaceAtRandom(int columns, int rows, IReadOnlyCollection<Position> occupied = null)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            int totalCells = columns * rows;
            if (occupied != null && occupied.Count >= totalCells)
                throw new InvalidOperationException("No free cell available for food.");

            if (occupied == null || occupied.Count == 0)
            {
                Location = new Position(_rng.Next(columns), _rng.Next(rows));
                return;
            }

            var blocked = new HashSet<Position>(occupied);

            // Try random positions first
            const int attemptLimit = 1000;
            for (int i = 0; i < attemptLimit; i++)
            {

                var candidate = new Position(_rng.Next(1, columns-1), _rng.Next(1, rows-1));
                if (!blocked.Contains(candidate))
                {
                    Location = candidate;
                    return;
                }
            }

            // Fallback: first free cell by scanning
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    var candidate = new Position(x, y);
                    if (!blocked.Contains(candidate))
                    {
                        Location = candidate;
                        return;
                    }
                }
            }

            throw new InvalidOperationException("No free cell found for food.");
        }
        public bool IsAt(Position p) => Location == p;
    }
}
