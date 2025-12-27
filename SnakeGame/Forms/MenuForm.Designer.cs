namespace SnakeGame.Forms
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLeaderBoard = new System.Windows.Forms.Button();
            this.btnQuitGame = new System.Windows.Forms.Button();
            this.Background2Rad = new System.Windows.Forms.RadioButton();
            this.Background1Rad = new System.Windows.Forms.RadioButton();
            this.BlueSnakeRad = new System.Windows.Forms.RadioButton();
            this.RedSnakeRad = new System.Windows.Forms.RadioButton();
            this.DefaultSnakeRad = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlueSnakeRad);
            this.groupBox1.Controls.Add(this.RedSnakeRad);
            this.groupBox1.Controls.Add(this.DefaultSnakeRad);
            this.groupBox1.Font = new System.Drawing.Font("Pristina", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Your Champion";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Background2Rad);
            this.groupBox2.Controls.Add(this.Background1Rad);
            this.groupBox2.Font = new System.Drawing.Font("Pristina", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose Your Background";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnStart.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(314, 376);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(180, 62);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLeaderBoard
            // 
            this.btnLeaderBoard.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLeaderBoard.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeaderBoard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLeaderBoard.Location = new System.Drawing.Point(77, 376);
            this.btnLeaderBoard.Name = "btnLeaderBoard";
            this.btnLeaderBoard.Size = new System.Drawing.Size(180, 62);
            this.btnLeaderBoard.TabIndex = 3;
            this.btnLeaderBoard.Text = "Leader Board";
            this.btnLeaderBoard.UseVisualStyleBackColor = false;
            this.btnLeaderBoard.Click += new System.EventHandler(this.btnLeaderBoard_Click);
            // 
            // btnQuitGame
            // 
            this.btnQuitGame.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnQuitGame.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuitGame.Location = new System.Drawing.Point(546, 376);
            this.btnQuitGame.Name = "btnQuitGame";
            this.btnQuitGame.Size = new System.Drawing.Size(180, 62);
            this.btnQuitGame.TabIndex = 4;
            this.btnQuitGame.Text = "Quit Game";
            this.btnQuitGame.UseVisualStyleBackColor = false;
            this.btnQuitGame.Click += new System.EventHandler(this.btnQuitGame_Click);
            // 
            // Background2Rad
            // 
            this.Background2Rad.AutoSize = true;
            this.Background2Rad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Background2Rad.Image = global::SnakeGame.Properties.Resources.GameBackground2_ic;
            this.Background2Rad.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Background2Rad.Location = new System.Drawing.Point(534, 51);
            this.Background2Rad.Name = "Background2Rad";
            this.Background2Rad.Size = new System.Drawing.Size(145, 85);
            this.Background2Rad.TabIndex = 1;
            this.Background2Rad.TabStop = true;
            this.Background2Rad.UseVisualStyleBackColor = true;
            this.Background2Rad.CheckedChanged += new System.EventHandler(this.Background2Rad_CheckedChanged);
            // 
            // Background1Rad
            // 
            this.Background1Rad.AutoSize = true;
            this.Background1Rad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Background1Rad.Image = global::SnakeGame.Properties.Resources.GameBackground1_ic;
            this.Background1Rad.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Background1Rad.Location = new System.Drawing.Point(82, 56);
            this.Background1Rad.Name = "Background1Rad";
            this.Background1Rad.Size = new System.Drawing.Size(145, 80);
            this.Background1Rad.TabIndex = 0;
            this.Background1Rad.TabStop = true;
            this.Background1Rad.UseVisualStyleBackColor = true;
            this.Background1Rad.CheckedChanged += new System.EventHandler(this.Background1Rad_CheckedChanged);
            // 
            // BlueSnakeRad
            // 
            this.BlueSnakeRad.AutoSize = true;
            this.BlueSnakeRad.Image = global::SnakeGame.Properties.Resources.BlueHead;
            this.BlueSnakeRad.Location = new System.Drawing.Point(534, 17);
            this.BlueSnakeRad.Name = "BlueSnakeRad";
            this.BlueSnakeRad.Size = new System.Drawing.Size(145, 128);
            this.BlueSnakeRad.TabIndex = 2;
            this.BlueSnakeRad.TabStop = true;
            this.BlueSnakeRad.UseVisualStyleBackColor = true;
            this.BlueSnakeRad.CheckedChanged += new System.EventHandler(this.BlueSnakeRad_CheckedChanged);
            // 
            // RedSnakeRad
            // 
            this.RedSnakeRad.AutoSize = true;
            this.RedSnakeRad.Image = global::SnakeGame.Properties.Resources.RedHead;
            this.RedSnakeRad.Location = new System.Drawing.Point(302, 17);
            this.RedSnakeRad.Name = "RedSnakeRad";
            this.RedSnakeRad.Size = new System.Drawing.Size(145, 128);
            this.RedSnakeRad.TabIndex = 1;
            this.RedSnakeRad.TabStop = true;
            this.RedSnakeRad.UseVisualStyleBackColor = true;
            this.RedSnakeRad.CheckedChanged += new System.EventHandler(this.RedSnakeRad_CheckedChanged);
            // 
            // DefaultSnakeRad
            // 
            this.DefaultSnakeRad.AutoSize = true;
            this.DefaultSnakeRad.Image = global::SnakeGame.Properties.Resources.DefaultSnakeHead;
            this.DefaultSnakeRad.Location = new System.Drawing.Point(82, 17);
            this.DefaultSnakeRad.Name = "DefaultSnakeRad";
            this.DefaultSnakeRad.Size = new System.Drawing.Size(145, 128);
            this.DefaultSnakeRad.TabIndex = 0;
            this.DefaultSnakeRad.TabStop = true;
            this.DefaultSnakeRad.UseVisualStyleBackColor = true;
            this.DefaultSnakeRad.CheckedChanged += new System.EventHandler(this.DefaultSnakeRad_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Vừa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Map khó";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuitGame);
            this.Controls.Add(this.btnLeaderBoard);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RedSnakeRad;
        private System.Windows.Forms.RadioButton DefaultSnakeRad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton Background1Rad;
        private System.Windows.Forms.RadioButton BlueSnakeRad;
        private System.Windows.Forms.RadioButton Background2Rad;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLeaderBoard;
        private System.Windows.Forms.Button btnQuitGame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}