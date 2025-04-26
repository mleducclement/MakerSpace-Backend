using FluentValidation;
using MakerSpace.Application.Products.Dtos;
using MakerSpace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Application.Products.Validators;

public class ProductMutateDtoValidator : AbstractValidator<ProductMutateDto> {
   public ProductMutateDtoValidator(AppDbContext db) {
      RuleFor(x => x.Sku)
         .NotEmpty().WithMessage("Sku is required")
         .Length(1, 30).WithMessage("Sku must be between 1 and 30 characters");

      RuleFor(x => x.Name)
         .NotEmpty().WithMessage("Name is required")
         .Length(1, 50).WithMessage("Name must be between 1 and 50 characters");

      RuleFor(x => x.Description)
         .NotEmpty().WithMessage("Description is required")
         .Length(1, 250).WithMessage("Description must be between 1 and 250 characters");

      RuleFor(x => x.CategorySlug)
         .MustAsync(async (slug, cancellation) => {
            return await db.Categories
               .AnyAsync(c => c.Slug == slug, cancellation);
         }).WithMessage($"Category doesn't exist");
      
      RuleFor(x => x.Price)
         .GreaterThan(0).WithMessage("Price must be greater than 0")
         .LessThan(1000).WithMessage("Price must be less than 1000");

      RuleFor(x => x.ImageUri)
         .NotEmpty().WithMessage("ImageUri is required")
         .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
         .Length(1, 500).WithMessage("ImageUri must be between 1 and 500 characters")
         .WithMessage("ImageUri must be a valid URI");
      
      RuleFor(x => x.Rating)
         .GreaterThanOrEqualTo(0).WithMessage("Rating not be lower than 0")
         .LessThanOrEqualTo(5).WithMessage("Rating must be less than or equal to 5");
      
      RuleFor(x => x.PromoRate)
         .GreaterThanOrEqualTo(0).WithMessage("Promo Rate cannot be lower than 0")
         .LessThanOrEqualTo(100).WithMessage("Promo Rate must be less than or equal to 100");

      RuleFor(x => x.Stock)
         .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be lower than 0");
   }
}