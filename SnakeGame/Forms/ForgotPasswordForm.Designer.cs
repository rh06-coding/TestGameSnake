namespace SnakeGame.Forms
{
    partial class ForgotPasswordForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.EmailTxt = new System.Windows.Forms.TextBox();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.ResetPasswordTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(126, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your email";
            // 
            // EmailTxt
            // 
            this.EmailTxt.Location = new System.Drawing.Point(306, 112);
            this.EmailTxt.Name = "EmailTxt";
            this.EmailTxt.Size = new System.Drawing.Size(305, 22);
            this.EmailTxt.TabIndex = 1;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.BackColor = System.Drawing.Color.White;
            this.ConfirmBtn.Font = new System.Drawing.Font("Pristina", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmBtn.Location = new System.Drawing.Point(518, 264);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(93, 40);
            this.ConfirmBtn.TabIndex = 2;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(141, 201);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(120, 27);
            this.label.TabIndex = 3;
            this.label.Text = "Reset Password";
            // 
            // ResetPasswordTxt
            // 
            this.ResetPasswordTxt.Location = new System.Drawing.Point(306, 206);
            this.ResetPasswordTxt.Name = "ResetPasswordTxt";
            this.ResetPasswordTxt.Size = new System.Drawing.Size(305, 22);
            this.ResetPasswordTxt.TabIndex = 4;
            // 
            // ForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResetPasswordTxt);
            this.Controls.Add(this.label);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.EmailTxt);
            this.Controls.Add(this.label1);
            this.Name = "ForgotPassword";
            this.Text = "ForgotPassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EmailTxt;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox ResetPasswordTxt;
    }
}