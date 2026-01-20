using System;
using System.Text.RegularExpressions;

namespace SnakeGame.Validation
{
    public static class TaiKhoanValidator
    {
        public static ValidationResult ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return ValidationResult.Error("Username không được để trống");

            if (username.Length < 3)
                return ValidationResult.Error("Username phải có ít nhất 3 ký tự");

            if (username.Length > 50)
                return ValidationResult.Error("Username không được quá 50 ký tự");

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return ValidationResult.Error("Username chỉ được chứa chữ cái, số và dấu gạch dưới");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("Mật khẩu không được để trống");

            if (password.Length < 6)
                return ValidationResult.Error("Mật khẩu phải có ít nhất 6 ký tự");

            if (password.Length > 100)
                return ValidationResult.Error("Mật khẩu không được quá 100 ký tự");

            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
            bool hasLower = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");

            if (!hasUpper || !hasLower || !hasDigit)
                return ValidationResult.Error("Mật khẩu phải chứa ít nhất một chữ hoa, một chữ thường và một chữ số");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePasswordSimple(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("Mật khẩu không được để trống");

            if (password.Length < 6)
                return ValidationResult.Error("Mật khẩu phải có ít nhất 6 ký tự");

            if (password.Length > 100)
                return ValidationResult.Error("Mật khẩu không được quá 100 ký tự");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ValidationResult.Error("Email không được để trống");

            if (email.Length > 255)
                return ValidationResult.Error("Email không được quá 255 ký tự");

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, pattern))
                return ValidationResult.Error("Email không đúng định dạng");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateRegistration(string username, string password, string email)
        {
            var usernameResult = ValidateUsername(username);
            if (!usernameResult.IsValid)
                return usernameResult;

            var passwordResult = ValidatePasswordSimple(password);
            if (!passwordResult.IsValid)
                return passwordResult;

            var emailResult = ValidateEmail(email);
            if (!emailResult.IsValid)
                return emailResult;

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return ValidationResult.Error("Username không được để trống");

            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("Mật khẩu không được để trống");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePlayerID(int playerID)
        {
            if (playerID <= 0)
                return ValidationResult.Error("Player ID không hợp lệ");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateScore(int score)
        {
            if (score < 0)
                return ValidationResult.Error("Điểm số không được âm");

            if (score > 999999)
                return ValidationResult.Error("Điểm số không hợp lệ (quá lớn)");

            return ValidationResult.Success();
        }
    }
}
