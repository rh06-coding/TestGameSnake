using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Database;

namespace SnakeGame.Forms
{
    public partial class LeaderBoardForm : Form
    {
        private ScoreRepository _scoreRepository;

        public LeaderBoardForm()
        {
            InitializeComponent();
            _scoreRepository = new ScoreRepository();
            this.Load += LeaderBoardForm_Load;
        }

        private void LeaderBoardForm_Load(object sender, EventArgs e)
        {
            LoadLeaderBoard();
        }

        private void LoadLeaderBoard()
        {
            try
            {
                // Xóa dữ liệu cũ
                dgvLeaderBoard.Rows.Clear();

                // Lấy top 10 điểm cao nhất
                var topScores = _scoreRepository.GetTopScores(10);

                // Thêm dữ liệu vào DataGridView
                int rank = 1;
                foreach (var score in topScores)
                {
                    dgvLeaderBoard.Rows.Add(
                        rank,
                        score.Username,
                        score.HighestScore
                    );
                    rank++;
                }

                // Tùy chỉnh hiển thị
                if (dgvLeaderBoard.Rows.Count > 0)
                {
                    // Top 1 - Gold
                    dgvLeaderBoard.Rows[0].DefaultCellStyle.BackColor = Color.Gold;
                    dgvLeaderBoard.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                    dgvLeaderBoard.Rows[0].DefaultCellStyle.Font = new Font(dgvLeaderBoard.Font, FontStyle.Bold);

                    // Top 2 - Silver
                    if (dgvLeaderBoard.Rows.Count > 1)
                    {
                        dgvLeaderBoard.Rows[1].DefaultCellStyle.BackColor = Color.Silver;
                        dgvLeaderBoard.Rows[1].DefaultCellStyle.ForeColor = Color.Black;
                        dgvLeaderBoard.Rows[1].DefaultCellStyle.Font = new Font(dgvLeaderBoard.Font, FontStyle.Bold);
                    }

                    // Top 3 - Bronze
                    if (dgvLeaderBoard.Rows.Count > 2)
                    {
                        dgvLeaderBoard.Rows[2].DefaultCellStyle.BackColor = Color.SandyBrown;
                        dgvLeaderBoard.Rows[2].DefaultCellStyle.ForeColor = Color.Black;
                        dgvLeaderBoard.Rows[2].DefaultCellStyle.Font = new Font(dgvLeaderBoard.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải bảng xếp hạng: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnExitToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog();
        }
    }
}
