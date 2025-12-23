namespace SnakeGame
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.GameCanvas = new System.Windows.Forms.Panel();
            this.PauseMenuPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.QuitToMenuBtn = new System.Windows.Forms.Button();
            this.ResumeBtn = new System.Windows.Forms.Button();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.PauseBtn = new System.Windows.Forms.Button();
            this.GameCanvas.SuspendLayout();
            this.PauseMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameCanvas
            // 
            this.GameCanvas.BackColor = System.Drawing.Color.Black;
            this.GameCanvas.BackgroundImage = global::SnakeGame.Properties.Resources.GameBackground1;
            this.GameCanvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameCanvas.Controls.Add(this.PauseMenuPanel);
            this.GameCanvas.Location = new System.Drawing.Point(0, 0);
            this.GameCanvas.Name = "GameCanvas";
            this.GameCanvas.Size = new System.Drawing.Size(740, 640);
            this.GameCanvas.TabIndex = 0;
            this.GameCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.GameCanvas_Paint);
            // 
            // PauseMenuPanel
            // 
            this.PauseMenuPanel.BackColor = System.Drawing.Color.White;
            this.PauseMenuPanel.Controls.Add(this.label1);
            this.PauseMenuPanel.Controls.Add(this.QuitToMenuBtn);
            this.PauseMenuPanel.Controls.Add(this.ResumeBtn);
            this.PauseMenuPanel.Location = new System.Drawing.Point(300, 200);
            this.PauseMenuPanel.Name = "PauseMenuPanel";
            this.PauseMenuPanel.Size = new System.Drawing.Size(400, 300);
            this.PauseMenuPanel.TabIndex = 3;
            this.PauseMenuPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pristina", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Game Paused";
            // 
            // QuitToMenuBtn
            // 
            this.QuitToMenuBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitToMenuBtn.Location = new System.Drawing.Point(67, 176);
            this.QuitToMenuBtn.Name = "QuitToMenuBtn";
            this.QuitToMenuBtn.Size = new System.Drawing.Size(89, 65);
            this.QuitToMenuBtn.TabIndex = 1;
            this.QuitToMenuBtn.TabStop = false;
            this.QuitToMenuBtn.Text = "Quit To Menu";
            this.QuitToMenuBtn.UseVisualStyleBackColor = true;
            this.QuitToMenuBtn.Click += new System.EventHandler(this.QuitToMenuBtn_Click);
            // 
            // ResumeBtn
            // 
            this.ResumeBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeBtn.Location = new System.Drawing.Point(248, 176);
            this.ResumeBtn.Name = "ResumeBtn";
            this.ResumeBtn.Size = new System.Drawing.Size(89, 65);
            this.ResumeBtn.TabIndex = 0;
            this.ResumeBtn.TabStop = false;
            this.ResumeBtn.Text = "Resume";
            this.ResumeBtn.UseVisualStyleBackColor = true;
            this.ResumeBtn.Click += new System.EventHandler(this.ResumeBtn_Click);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.Location = new System.Drawing.Point(740, 9);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(128, 38);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "Score: ";
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 150;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // PauseBtn
            // 
            this.PauseBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseBtn.Location = new System.Drawing.Point(834, 180);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(75, 64);
            this.PauseBtn.TabIndex = 2;
            this.PauseBtn.TabStop = false;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.GameCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_Closing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_Closed);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.GameCanvas.ResumeLayout(false);
            this.PauseMenuPanel.ResumeLayout(false);
            this.PauseMenuPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel GameCanvas;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Panel PauseMenuPanel;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button QuitToMenuBtn;
        private System.Windows.Forms.Button ResumeBtn;
        private System.Windows.Forms.Label label1;
    }
}

