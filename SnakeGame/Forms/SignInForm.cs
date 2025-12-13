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
    public partial class SignInForm : Form
    {
        private TaiKhoanRepository _taiKhoanRepo;

        public SignInForm()
        {
            InitializeComponent();
            _taiKhoanRepo = new TaiKhoanRepository();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTxt.Text.Trim();
            string email = EmailTxt.Text.Trim();
            string password = PasswordTxt.Text;

            var validationResult = TaiKhoanValidator.ValidateRegistration(username, password, email);
            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _taiKhoanRepo.Register(username, password, email);
                if (success)
                {
                    MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
