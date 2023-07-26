using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public RegisterValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor (r => r.FirstName)
            .NotEmpty().WithMessage("First Name is required");

        RuleFor (r => r.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10)); //harus berumur lebih dari 10 tahun

        RuleFor(r => r.Gender)
            .NotNull()
            .IsInEnum(); //bergantung dengan banyaknya enum

        RuleFor(r => r.HiringDate)
            .NotEmpty().WithMessage("Hiring date is required");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .Must(IsDuplicateValue).WithMessage("Email already exists");

        RuleFor(r => r.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(20).WithMessage("Maximum 20 characters allowed")
            .Matches(@"^\+[0-9]+$")
            .Must(IsDuplicateValue).WithMessage("Phone number already exists");

        RuleFor(r => r.UniversityCode)
            .NotEmpty().WithMessage("Code is required");

        RuleFor(r => r.UniversityName)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(r => r.Major)
            .NotEmpty().WithMessage("Major is required");

        RuleFor(r => r.Degree)
            .NotEmpty().WithMessage("Degree is required");

        RuleFor(r => r.Gpa)
            .NotEmpty().WithMessage("Gpa is required");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Minimum 8 characters allowed")
            .Must(HasCapitalLetter).WithMessage("Password must contain at least one capital letter")
            .Must(HasNumber).WithMessage("Password must contain at least one number");

        RuleFor(r => r.RepeatPassword)
            .Equal(r => r.Password).WithMessage("Password does not match");
    }

    private bool IsDuplicateValue(string arg)
    {

        return _employeeRepository.IsNotExist(arg);
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
