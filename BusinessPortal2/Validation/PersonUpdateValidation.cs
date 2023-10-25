using BusinessPortal2.Controllers;
using BusinessPortal2.Data;
using BusinessPortal2.Models.DTO.PersonalDTO;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Validation
{
    public class PersonUpdateValidation : AbstractValidator<PersonalUpdateDTO>
    {
        private readonly PersonaldataContext _context;
        public PersonUpdateValidation(PersonaldataContext context)
        {
            _context = context;

            RuleFor(model => model.FullName)
                .NotEmpty().WithMessage("Full Name is required.");
            //    .MinimumLength(2).WithMessage("Full Name must be at least 2 characters long.")
            //    .MaximumLength(40).WithMessage("Full Name cannot exceed 40 characters.");
            //RuleFor(model => model.Email)
            //    .NotEmpty().WithMessage("Email is required.")
            //    .EmailAddress().WithMessage("Invalid email format.")
            //    .MustAsync(async (Email, cancellationToken) => await EmailExists(Email, cancellationToken))
            //    .WithMessage("Email already exists. Please use a different email address.");
        }

        public async Task<bool> EmailExists(string email, CancellationToken cToken)
        {
            return !(await _context.personals.AnyAsync(e => e.Email.ToLower() == email.ToLower(), cToken));
        }
    }
}
