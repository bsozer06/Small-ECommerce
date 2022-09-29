using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please you should write the name.")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Please you enter the letters between 2 and 150");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please you should enter the stock.")
                .Must(s => s >= 0)
                    .WithMessage("Stock must not be the negative.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please you should enter the price.")
                .Must(s => s >= 0)
                    .WithMessage("Price must not be the negative.");
        }
    }
}
