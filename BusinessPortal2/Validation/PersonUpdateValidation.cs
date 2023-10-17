using BusinessPortal2.Models.DTO.PersonalDTO;
using FluentValidation;

namespace BusinessPortal2.Validation
{
    public class PersonUpdateValidation : AbstractValidator<PersonalUpdateDTO>
    {
        public PersonUpdateValidation()
        {
            RuleFor(model => model.FullName).NotEmpty().MinimumLength(4).MaximumLength(40);
            RuleFor(model => model.Email).NotEmpty().EmailAddress();
        }
    }
}
