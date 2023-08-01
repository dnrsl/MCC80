using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public UpdateEmployeeValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(a => a.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("First name is required");

        RuleFor(e => e.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10)); //harus berumur lebih dari 10 tahun

        RuleFor(e => e.Gender)
            .NotNull()
            .IsInEnum(); //bergantung dengan banyaknya enum

        RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage("Hiring date is required");


        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .Must(IsDuplicateOrSame).WithMessage("Email already exists");


        RuleFor(e => e.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(20).WithMessage("Maximum 20 characters allowed")
            .Matches(@"^\+[0-9]+$")
            .Must(IsDuplicateOrSame).WithMessage("Phone number already exists");
    }

    //custom method

    private bool IsDuplicateOrSame (string arg)
    {
        return _employeeRepository.IsDataUnique(arg);
    }
}
