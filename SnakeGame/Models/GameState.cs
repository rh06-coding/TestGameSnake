using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SnakeGame.Service;

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

        public GameState(int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            Columns = columns;
            Rows = rows;

            Reset();
        }

        public void Reset()
        {
            IsGameOver = false;
            Score = 0;
            var start = new Position(Columns / 2, Rows / 2);
            Snake = new Snake(start, Direction.Huong.Right);
            Food = new Food();
            SpawnFood();
        }

        public void ChangeDirection(Direction.Huong newDirection)
        {
            Snake.ChangeDirection(newDirection);
        }

        //Tick 
        public bool Tick()
        {
            if (IsGameOver) return false;

            //Di chuyen ran phia truoc
            Snake.Move();
            var head = Snake.Body[0];

            //Kiem tra va cham tuong
            if(head.X < 0 || head.X >= Columns || head.Y < 0 || head.Y >= Rows)
            {
                IsGameOver = true;
                SoundService.PlayLose();
                return false;
            }

            //check self-collision
            if(Snake.Body.Skip(1).Any(p => p == head))
            {
                IsGameOver = true;
                return false;
            }

            //Kiem tra an thuc an
            if(Food.IsAt(head))
            {
                SoundService.PlayEat();
                Snake.Grow();
                Score += 10;
                SpawnFood();
            }

            return true;
        }

        private void SpawnFood()
        {
            Food.PlaceAtRandom(Columns, Rows, Snake.Body);
        }
    }
}
