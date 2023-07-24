using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities;

public class UniversityValidator : AbstractValidator<UniversityDto>
{
    public UniversityValidator()
    {
        RuleFor(u => u.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(u => u.Code)
            .NotEmpty().WithMessage("Code is required");

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}
