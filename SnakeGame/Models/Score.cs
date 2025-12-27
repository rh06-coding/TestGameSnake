using System;

namespace SnakeGame.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int PlayerID { get; set; }
        public int ScoreValue { get; set; }
        public DateTime AchievedAt { get; set; }
        public int MapType { get; set; }

        public Score()
        {
            AchievedAt = DateTime.Now;
            MapType = 1;
        }
    }
}
