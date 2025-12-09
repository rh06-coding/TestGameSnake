using System;

namespace SnakeGame.Models
{
    public class TaiKhoan
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public int HighestScore { get; set; }

        public TaiKhoan()
        {
            JoinDate = DateTime.Now;
            HighestScore = 0;
        }
    }
}
