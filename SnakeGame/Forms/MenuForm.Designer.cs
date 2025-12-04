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
            this.BlueSnakeRad = new System.Windows.Forms.RadioButton();
            this.RedSnakeRad = new System.Windows.Forms.RadioButton();
            this.DefaultSnakeRad = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Background1Rad = new System.Windows.Forms.RadioButton();
            this.Background2Rad = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlueSnakeRad);
            this.groupBox1.Controls.Add(this.RedSnakeRad);
            this.groupBox1.Controls.Add(this.DefaultSnakeRad);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hãy chọn tạo hình rắn";
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
            this.RedSnakeRad.Location = new System.Drawing.Point(311, 17);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Background2Rad);
            this.groupBox2.Controls.Add(this.Background1Rad);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hãy chọn Background";
            // 
            // Background1Rad
            // 
            this.Background1Rad.AutoSize = true;
            this.Background1Rad.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Background1Rad.Location = new System.Drawing.Point(82, 61);
            this.Background1Rad.Name = "Background1Rad";
            this.Background1Rad.Size = new System.Drawing.Size(107, 20);
            this.Background1Rad.TabIndex = 0;
            this.Background1Rad.TabStop = true;
            this.Background1Rad.Text = "Backgound 1";
            this.Background1Rad.UseVisualStyleBackColor = true;
            // 
            // Background2Rad
            // 
            this.Background2Rad.AutoSize = true;
            this.Background2Rad.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Background2Rad.Location = new System.Drawing.Point(534, 61);
            this.Background2Rad.Name = "Background2Rad";
            this.Background2Rad.Size = new System.Drawing.Size(107, 20);
            this.Background2Rad.TabIndex = 1;
            this.Background2Rad.TabStop = true;
            this.Background2Rad.Text = "Backgound 2";
            this.Background2Rad.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(323, 383);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 55);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
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
        private System.Windows.Forms.Button button1;
    }
}