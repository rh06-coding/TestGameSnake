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
using SnakeGame.Models;
using SnakeGame.Validation;

namespace SnakeGame.Forms
{
    public partial class StartForm : Form
    {
        private TaiKhoanRepository _taiKhoanRepo;

        public StartForm()
        {
            InitializeComponent();
            _taiKhoanRepo = new TaiKhoanRepository();
            passwordTxt.UseSystemPasswordChar = true;
        }

        private void FPBtn_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm();
            forgotPasswordForm.ShowDialog();
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.ShowDialog();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTxt.Text.Trim();
            string password = passwordTxt.Text;

            var validationResult = TaiKhoanValidator.ValidateLogin(username, password);
            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TaiKhoan user = _taiKhoanRepo.Login(username, password);
                if (user != null)
                {
                    SessionManager.Login(user);
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {user.Username}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Hide();
                    MenuForm menuForm = new MenuForm();
                    menuForm.FormClosed += (s, args) => this.Close();
                    menuForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
