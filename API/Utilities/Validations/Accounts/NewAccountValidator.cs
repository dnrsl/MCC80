using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class NewAccountValidator : AbstractValidator<NewAccountDto>
{
    public NewAccountValidator()
    {
        RuleFor(a => a.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Minimum 8 characters allowed")
            .Must(HasCapitalLetter).WithMessage("Password must contain at least one capital letter")
            .Must(HasNumber).WithMessage("Password must contain at least one number");

        RuleFor(a => a.Otp)
            .NotEmpty().WithMessage("Otp is required");

        RuleFor(a => a.IsUsed)
            .NotEmpty().WithMessage("IsUsed is required");

        RuleFor(a => a.ExpiredTime)
            .NotEmpty().WithMessage("Expired Date is required")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Expired date cannot be earlier than current date");
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
