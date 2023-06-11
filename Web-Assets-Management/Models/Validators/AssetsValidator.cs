using FluentValidation;

namespace Web_Assets_Management.Models.Validators
{
    public class AssetsValidator: AbstractValidator<Assets>
    {
        public AssetsValidator()
        {
            RuleFor(x => x.AssetsNumber).NotNull();
            RuleFor(x => x.AssetsNumber).GreaterThan(1);
            RuleFor(x => x.AssetsNumber).LessThan(2147483647);
            RuleFor(x => x.CategorieName).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Price).LessThan(2147483647);
            RuleFor(x => x.Price).GreaterThan(1);
            RuleFor(x => x.Vendor).NotNull();
        }
    }
}
