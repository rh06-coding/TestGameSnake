using System;
using System.Text.RegularExpressions;

namespace SnakeGame.Validation
{
    public static class TaiKhoanValidator
    {
        public static ValidationResult ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return ValidationResult.Error("Username không ???c ?? tr?ng");

            if (username.Length < 3)
                return ValidationResult.Error("Username ph?i có ít nh?t 3 ký t?");

            if (username.Length > 50)
                return ValidationResult.Error("Username không ???c quá 50 ký t?");

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return ValidationResult.Error("Username ch? ???c ch?a ch? cái, s? và d?u g?ch d??i");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("M?t kh?u không ???c ?? tr?ng");

            if (password.Length < 6)
                return ValidationResult.Error("M?t kh?u ph?i có ít nh?t 6 ký t?");

            if (password.Length > 100)
                return ValidationResult.Error("M?t kh?u không ???c quá 100 ký t?");

            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
            bool hasLower = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");

            if (!hasUpper || !hasLower || !hasDigit)
                return ValidationResult.Error("M?t kh?u ph?i ch?a ít nh?t m?t ch? hoa, m?t ch? th??ng và m?t ch? s?");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePasswordSimple(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("M?t kh?u không ???c ?? tr?ng");

            if (password.Length < 6)
                return ValidationResult.Error("M?t kh?u ph?i có ít nh?t 6 ký t?");

            if (password.Length > 100)
                return ValidationResult.Error("M?t kh?u không ???c quá 100 ký t?");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ValidationResult.Error("Email không ???c ?? tr?ng");

            if (email.Length > 255)
                return ValidationResult.Error("Email không ???c quá 255 ký t?");

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, pattern))
                return ValidationResult.Error("Email không ?úng ??nh d?ng");

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
                return ValidationResult.Error("Username không ???c ?? tr?ng");

            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Error("M?t kh?u không ???c ?? tr?ng");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidatePlayerID(int playerID)
        {
            if (playerID <= 0)
                return ValidationResult.Error("Player ID không h?p l?");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateScore(int score)
        {
            if (score < 0)
                return ValidationResult.Error("?i?m s? không ???c âm");

            if (score > 999999)
                return ValidationResult.Error("?i?m s? không h?p l? (quá l?n)");

            return ValidationResult.Success();
        }
    }
}
