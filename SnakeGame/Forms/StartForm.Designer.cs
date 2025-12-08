namespace SnakeGame.Forms
{
    partial class StartForm
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
            this.UsernameTxt = new System.Windows.Forms.TextBox();
            this.passwordTxt = new System.Windows.Forms.MaskedTextBox();
            this.Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.SignInBtn = new System.Windows.Forms.Button();
            this.FPBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UsernameTxt
            // 
            this.UsernameTxt.Location = new System.Drawing.Point(369, 259);
            this.UsernameTxt.Name = "UsernameTxt";
            this.UsernameTxt.Size = new System.Drawing.Size(230, 22);
            this.UsernameTxt.TabIndex = 0;
            // 
            // passwordTxt
            // 
            this.passwordTxt.Location = new System.Drawing.Point(369, 308);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(230, 22);
            this.passwordTxt.TabIndex = 1;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Label.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Location = new System.Drawing.Point(158, 250);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(192, 31);
            this.Label.TabIndex = 2;
            this.Label.Text = "Enter username or email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.label1.Font = new System.Drawing.Font("Pristina", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(221, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter password";
            // 
            // LoginBtn
            // 
            this.LoginBtn.BackColor = System.Drawing.Color.PaleTurquoise;
            this.LoginBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginBtn.Location = new System.Drawing.Point(513, 353);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(86, 64);
            this.LoginBtn.TabIndex = 4;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = false;
            // 
            // SignInBtn
            // 
            this.SignInBtn.BackColor = System.Drawing.Color.PaleTurquoise;
            this.SignInBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInBtn.Location = new System.Drawing.Point(395, 353);
            this.SignInBtn.Name = "SignInBtn";
            this.SignInBtn.Size = new System.Drawing.Size(86, 64);
            this.SignInBtn.TabIndex = 5;
            this.SignInBtn.Text = "Sign In";
            this.SignInBtn.UseVisualStyleBackColor = false;
            // 
            // FPBtn
            // 
            this.FPBtn.BackColor = System.Drawing.Color.PaleTurquoise;
            this.FPBtn.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPBtn.Location = new System.Drawing.Point(283, 353);
            this.FPBtn.Name = "FPBtn";
            this.FPBtn.Size = new System.Drawing.Size(86, 64);
            this.FPBtn.TabIndex = 6;
            this.FPBtn.Text = "Forgot Password";
            this.FPBtn.UseVisualStyleBackColor = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SnakeGame.Properties.Resources.SnakeGameStartScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FPBtn);
            this.Controls.Add(this.SignInBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.UsernameTxt);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameTxt;
        private System.Windows.Forms.MaskedTextBox passwordTxt;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button SignInBtn;
        private System.Windows.Forms.Button FPBtn;
    }
}