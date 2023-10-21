using FluentValidation;
using TaskManagement.Core.Enums;
using TaskManagement.Core.Requests;

namespace TaskManagement.API.Validators
{
    public class CreateUpdateTaskRequestValidator : AbstractValidator<CreateUpdateTaskRequest>
    {
        public CreateUpdateTaskRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is mandatory");
            RuleFor(x => x.Status)
                .Must(x => x == (int)TaskStatusEnum.New || x == (int)TaskStatusEnum.InProgress || x == (int)TaskStatusEnum.Completed)
                .NotNull()
                .WithMessage("Status is mandatory and must be 0, 1, 2");
        }
    }
}
