using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Services;

namespace SnakeGame.Forms
{
    public partial class MenuForm : Form
    {
        int loaimap = 1;
        int mauran = 1;

        bool firstRan = true; 
        bool firstMap = true; 

        public MenuForm()
        {
            InitializeComponent();
            DefaultSnakeRad.Checked = true;
            Background1Rad.Checked = true;
        }

        private void DefaultSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            if (!firstRan)
            {
                SoundService.PlayChoose();
            }
            mauran = 1;
        }

        private void RedSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            SoundService.PlayChoose();

            firstRan = false;
            mauran = 2;
        }

        private void BlueSnakeRad_CheckedChanged(object sender, EventArgs e)
        {
            SoundService.PlayChoose();

            firstRan = false;
            mauran = 3;
        }

        private void Background1Rad_CheckedChanged(object sender, EventArgs e)
        {
            if (!firstMap)
            {
                SoundService.PlayChoose();
            }
            loaimap = 1;
        }

        private void Background2Rad_CheckedChanged(object sender, EventArgs e)
        {
            SoundService.PlayChoose();

            firstMap = false;
            loaimap = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundService.PlayClickButton();
            SoundService.StopBackground();

            GameForm form = new GameForm(loaimap, mauran);
            this.Hide();
            form.ShowDialog();
        }

        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            SoundService.PlayClickButton();
            Application.Exit();
        }

        private void btnLeaderBoard_Click(object sender, EventArgs e)
        {
            SoundService.PlayClickButton();
            LeaderBoardForm LeaderBoardform = new LeaderBoardForm();
            this.Hide();
            LeaderBoardform.ShowDialog();
        }
    }
}
