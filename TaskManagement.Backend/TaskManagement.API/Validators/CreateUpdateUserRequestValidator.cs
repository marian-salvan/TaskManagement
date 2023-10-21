using FluentValidation;
using TaskManagement.Core.Requests;

namespace TaskManagement.API.Validators
{
    public class CreateUpdateUserRequestValidator : AbstractValidator<CreateUpdateUserRequest>
    {
        public CreateUpdateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is mandatory");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Email address is mandatory");
        }
    }
}
