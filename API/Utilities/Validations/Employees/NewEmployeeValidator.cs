using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

public class NewEmployeeValidator : AbstractValidator<NewEmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public NewEmployeeValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

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
            .Must(IsDuplicateValue).WithMessage("Email already exists");

        RuleFor(e => e.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(20).WithMessage("Maximum 20 characters allowed")
            .Matches(@"^\+[0-9]$")
            .Must(IsDuplicateValue).WithMessage("Phone number already exists");
    }

    //custom method
    private bool IsDuplicateValue(string arg)
    {

        return _employeeRepository.IsNotExist(arg);
    }
}