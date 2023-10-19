using BusinessPortal2.Data;
using BusinessPortal2.Models.DTO.PersonalDTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Validation
{
    public class PersonalRegisterDTOValidation : AbstractValidator<RegisterPersonalDTO>
    {
        private readonly PersonaldataContext _context;
        public PersonalRegisterDTOValidation(PersonaldataContext context)
        {
            _context = context;

            RuleFor(model => model.FullName).NotEmpty().MinimumLength(4).MaximumLength(40);
            RuleFor(model => model.Email).NotEmpty().EmailAddress()
                .MustAsync(async (Email, cancellationToken) => await EmailExists(Email, cancellationToken))
                .WithMessage("Email already exist.");
            RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }

        public async Task<bool> EmailExists(string email, CancellationToken cToken)
        {
            return !(await _context.personals.AnyAsync(e => e.Email.ToLower() == email.ToLower(), cToken));
        }
    }
}
