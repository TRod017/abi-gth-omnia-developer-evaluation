using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

public class GetAllProductsRequestValidator : AbstractValidator<GetAllProductsRequest>
{
    public GetAllProductsRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("'Page' must be greater than 0.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("'Size' must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("'Size' must be less or equal to 100.");
    }
}
