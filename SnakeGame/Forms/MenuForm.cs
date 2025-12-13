using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Forms
{
    public partial class MenuForm : Form
    {
        int loaimap = 1;
        int mauran = 1;
        public MenuForm()
        {
            InitializeComponent();
        }

        private void DefaultSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            mauran = 1;
        }

        private void RedSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            mauran = 2;
        }

        private void BlueSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            mauran = 3;
        }

        private void Background1Rad_CheckedChanged(object sender, EventArgs e)
        {
            loaimap = 1;
        }

        private void Background2Rad_CheckedChanged(object sender, EventArgs e)
        {
            loaimap = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm form = new GameForm(loaimap, mauran);
            this.Hide();
            form.ShowDialog();
        }

        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLeaderBoard_Click(object sender, EventArgs e)
        {
            LeaderBoardForm LeaderBoardform = new LeaderBoardForm();
            this.Hide();
            LeaderBoardform.ShowDialog();
        }
    }
}
