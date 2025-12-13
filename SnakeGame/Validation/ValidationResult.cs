using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }
        public List<string> Errors { get; private set; }

        private ValidationResult(bool isValid, string errorMessage = "", List<string> errors = null)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
            Errors = errors ?? new List<string>();
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(true);
        }

        public static ValidationResult Error(string message)
        {
            return new ValidationResult(false, message, new List<string> { message });
        }

        public static ValidationResult FromErrors(List<string> errors)
        {
            return new ValidationResult(false, string.Join("; ", errors), errors);
        }

        public void AddError(string error)
        {
            IsValid = false;
            Errors.Add(error);
            ErrorMessage = string.Join("; ", Errors);
        }

        public static ValidationResult Combine(params ValidationResult[] results)
        {
            var allErrors = new List<string>();
            foreach (var result in results)
            {
                if (!result.IsValid)
                {
                    allErrors.AddRange(result.Errors);
                }
            }

            if (allErrors.Any())
            {
                return ValidationResult.FromErrors(allErrors);
            }

            return ValidationResult.Success();
        }
    }
}
