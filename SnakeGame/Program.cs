using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Database;

namespace SnakeGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Kiểm tra kết nối database trước khi chạy
            try
            {
                if (!DatabaseHelper.TestConnection())
                {
                    MessageBox.Show(
                        "Không thể khởi tạo database. Vui lòng kiểm tra log để biết thêm chi tiết.",
                        "Lỗi khởi động",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khởi động ứng dụng:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new SnakeGame.Forms.StartForm());
        }
    }
}
