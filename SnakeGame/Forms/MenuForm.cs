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
    }
}
