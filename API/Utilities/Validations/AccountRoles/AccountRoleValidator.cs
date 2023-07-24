using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
{
    public AccountRoleValidator()
    {
        RuleFor(ar => ar.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(ar => ar.AccountGuid)
            .NotEmpty().WithMessage("Account Guid is required");

        RuleFor(ar => ar.RoleGuid)
            .NotEmpty().WithMessage("Role guid is required");
    }
}
