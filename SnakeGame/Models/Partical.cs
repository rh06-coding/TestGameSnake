using System;

namespace SnakeGame.Models
{
    internal class Particle
    {
        public Position Position;
        public float OffsetX;
        public float OffsetY;
        public float VelocityX;
        public float VelocityY;
        public int Life;

        public Particle(Position pos, Random rng)
        {
            Position = pos;
            OffsetX = 0;
            OffsetY = 0;

            VelocityX = (float)(rng.NextDouble() * 2 - 1);
            VelocityY = (float)(rng.NextDouble() * 2 - 1);

            Life = rng.Next(10, 20);
        }

        public void Update()
        {
            OffsetX += VelocityX;
            OffsetY += VelocityY;
            Life--;
        }

        public bool IsDead => Life <= 0;
    }
}
