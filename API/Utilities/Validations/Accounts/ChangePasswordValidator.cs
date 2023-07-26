using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(cp => cp.Otp)
            .NotEmpty().WithMessage("Otp is required")
            .MinimumLength(6).WithMessage("Enter the 6-digit OTP code that has been sent");

        RuleFor(cp => cp.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(cp => cp.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Minimum 8 characters allowed")
            .Must(HasCapitalLetter).WithMessage("Password must contain at least one capital letter")
            .Must(HasNumber).WithMessage("Password must contain at least one number");

        RuleFor(cp => cp.ConfirmPassword)
            .Equal(cp => cp.NewPassword);
    }

    public bool HasCapitalLetter(string arg)
    {
        return arg.Any(char.IsUpper);
    }

    public bool HasNumber(string arg)
    {
        return arg.Any(char.IsDigit);
    }
}
