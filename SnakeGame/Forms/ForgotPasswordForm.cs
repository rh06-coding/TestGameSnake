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
using SnakeGame.Validation;

namespace SnakeGame.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        private TaiKhoanRepository _taiKhoanRepo;

        public ForgotPasswordForm()
        {
            InitializeComponent();
            _taiKhoanRepo = new TaiKhoanRepository();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string email = EmailTxt.Text.Trim();
            string newPassword = ResetPasswordTxt.Text;

            var emailResult = TaiKhoanValidator.ValidateEmail(email);
            if (!emailResult.IsValid)
            {
                MessageBox.Show(emailResult.ErrorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var passwordResult = TaiKhoanValidator.ValidatePasswordSimple(newPassword);
            if (!passwordResult.IsValid)
            {
                MessageBox.Show(passwordResult.ErrorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!_taiKhoanRepo.IsEmailExists(email))
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = _taiKhoanRepo.ResetPassword(email, newPassword);
                if (success)
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đặt lại mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
