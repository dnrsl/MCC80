using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class NewRoomValidator : AbstractValidator<NewRoomDto>
{
    public NewRoomValidator()
    {
        RuleFor(ro => ro.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(ro => ro.Floor)
            .NotEmpty().WithMessage("Floor is required");
        
        RuleFor(ro => ro.Capacity)
            .NotEmpty().WithMessage("Capacity is required");
    }
}
