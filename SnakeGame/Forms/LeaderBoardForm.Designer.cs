namespace SnakeGame.Forms
{
    partial class LeaderBoardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaderBoardForm));
            this.dgvLeaderBoard = new System.Windows.Forms.DataGridView();
            this.RankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HighestScoreColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExitToMenu = new System.Windows.Forms.Button();
            this.lblLeaderBoard = new System.Windows.Forms.Label();
            this.radMapVuaBoard = new System.Windows.Forms.RadioButton();
            this.radMapKhoBoard = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLeaderBoard
            // 
            this.dgvLeaderBoard.AllowUserToAddRows = false;
            this.dgvLeaderBoard.AllowUserToDeleteRows = false;
            this.dgvLeaderBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLeaderBoard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLeaderBoard.BackgroundColor = System.Drawing.Color.Black;
            this.dgvLeaderBoard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaderBoard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLeaderBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeaderBoard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RankColumn,
            this.UsernameColumn,
            this.HighestScoreColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeaderBoard.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLeaderBoard.Location = new System.Drawing.Point(12, 105);
            this.dgvLeaderBoard.MultiSelect = false;
            this.dgvLeaderBoard.Name = "dgvLeaderBoard";
            this.dgvLeaderBoard.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaderBoard.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLeaderBoard.RowHeadersWidth = 51;
            this.dgvLeaderBoard.RowTemplate.Height = 24;
            this.dgvLeaderBoard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaderBoard.Size = new System.Drawing.Size(934, 401);
            this.dgvLeaderBoard.TabIndex = 1;
            // 
            // RankColumn
            // 
            this.RankColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RankColumn.HeaderText = "Rank";
            this.RankColumn.MinimumWidth = 6;
            this.RankColumn.Name = "RankColumn";
            this.RankColumn.ReadOnly = true;
            // 
            // UsernameColumn
            // 
            this.UsernameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UsernameColumn.HeaderText = "Username";
            this.UsernameColumn.MinimumWidth = 6;
            this.UsernameColumn.Name = "UsernameColumn";
            this.UsernameColumn.ReadOnly = true;
            // 
            // HighestScoreColumn
            // 
            this.HighestScoreColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HighestScoreColumn.HeaderText = "Highest Score";
            this.HighestScoreColumn.MinimumWidth = 6;
            this.HighestScoreColumn.Name = "HighestScoreColumn";
            this.HighestScoreColumn.ReadOnly = true;
            // 
            // btnExitToMenu
            // 
            this.btnExitToMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnExitToMenu.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitToMenu.Location = new System.Drawing.Point(393, 522);
            this.btnExitToMenu.Name = "btnExitToMenu";
            this.btnExitToMenu.Size = new System.Drawing.Size(194, 55);
            this.btnExitToMenu.TabIndex = 2;
            this.btnExitToMenu.Text = "Exit To Menu";
            this.btnExitToMenu.UseVisualStyleBackColor = false;
            this.btnExitToMenu.Click += new System.EventHandler(this.btnExitToMenu_Click);
            // 
            // lblLeaderBoard
            // 
            this.lblLeaderBoard.AutoSize = true;
            this.lblLeaderBoard.Font = new System.Drawing.Font("Pristina", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaderBoard.ForeColor = System.Drawing.Color.Lime;
            this.lblLeaderBoard.Location = new System.Drawing.Point(400, 9);
            this.lblLeaderBoard.Name = "lblLeaderBoard";
            this.lblLeaderBoard.Size = new System.Drawing.Size(163, 39);
            this.lblLeaderBoard.TabIndex = 3;
            this.lblLeaderBoard.Text = "Leader Board";
            // 
            // radMapVuaBoard
            // 
            this.radMapVuaBoard.AutoSize = true;
            this.radMapVuaBoard.Font = new System.Drawing.Font("Pristina", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radMapVuaBoard.ForeColor = System.Drawing.Color.Lime;
            this.radMapVuaBoard.Location = new System.Drawing.Point(243, 51);
            this.radMapVuaBoard.Name = "radMapVuaBoard";
            this.radMapVuaBoard.Size = new System.Drawing.Size(134, 31);
            this.radMapVuaBoard.TabIndex = 4;
            this.radMapVuaBoard.TabStop = true;
            this.radMapVuaBoard.Text = "Normal Map";
            this.radMapVuaBoard.UseVisualStyleBackColor = true;
            this.radMapVuaBoard.CheckedChanged += new System.EventHandler(this.radMapVuaBoard_CheckedChanged);
            // 
            // radMapKhoBoard
            // 
            this.radMapKhoBoard.AutoSize = true;
            this.radMapKhoBoard.Font = new System.Drawing.Font("Pristina", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radMapKhoBoard.ForeColor = System.Drawing.Color.Lime;
            this.radMapKhoBoard.Location = new System.Drawing.Point(601, 51);
            this.radMapKhoBoard.Name = "radMapKhoBoard";
            this.radMapKhoBoard.Size = new System.Drawing.Size(116, 31);
            this.radMapKhoBoard.TabIndex = 5;
            this.radMapKhoBoard.TabStop = true;
            this.radMapKhoBoard.Text = "Hard Map";
            this.radMapKhoBoard.UseVisualStyleBackColor = true;
            // 
            // LeaderBoardForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(958, 589);
            this.Controls.Add(this.radMapKhoBoard);
            this.Controls.Add(this.radMapVuaBoard);
            this.Controls.Add(this.lblLeaderBoard);
            this.Controls.Add(this.btnExitToMenu);
            this.Controls.Add(this.dgvLeaderBoard);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LeaderBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leader Board";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLeaderBoard;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsernameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HighestScoreColumn;
        private System.Windows.Forms.Button btnExitToMenu;
        private System.Windows.Forms.Label lblLeaderBoard;
        private System.Windows.Forms.RadioButton radMapVuaBoard;
        private System.Windows.Forms.RadioButton radMapKhoBoard;
    }
}