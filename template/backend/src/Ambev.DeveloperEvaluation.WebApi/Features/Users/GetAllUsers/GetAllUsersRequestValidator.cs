using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers
{
    /// <summary>
    /// Validator for <see cref="GetAllUsersRequest"/> that defines pagination rules.
    /// </summary>
    /// <remarks>
    /// Ensures that:
    /// - <c>Page</c> is greater than 0
    /// - <c>Size</c> is between 1 and 100
    /// </remarks>
    public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUsersRequestValidator"/> class
        /// and sets up pagination rules for user listing requests.
        /// </summary>
        public GetAllUsersRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");
        }
    }
}
