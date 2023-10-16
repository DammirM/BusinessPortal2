using BusinessPortal2.Models.DTO;
using FluentValidation;

namespace BusinessPortal2.Validation
{
    public class PersonalRegisterDTOValidation : AbstractValidator<RegisterPersonalDTO>
    {
        public PersonalRegisterDTOValidation()
        {
            RuleFor(model => model.FullName).NotEmpty().MinimumLength(4).MaximumLength(40);
            RuleFor(model => model.Email).NotEmpty().EmailAddress();
            RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}
