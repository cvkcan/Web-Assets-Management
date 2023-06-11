using FluentValidation;
using Web_Assets_Management.Models.ViewModels;

namespace Web_Assets_Management.Models.Validators
{
    public class LoginVMValidator:AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.EId).NotEmpty().WithMessage("ID is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
