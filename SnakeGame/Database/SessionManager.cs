using SnakeGame.Models;

namespace SnakeGame.Database
{
    public static class SessionManager
    {
        public static TaiKhoan CurrentUser { get; set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void Login(TaiKhoan user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
